using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Moody.Common.Base;
using Moody.Common.Extensions;

namespace Moody.Snake.Model
{
    internal class SnakeLogic
    {
        private Field _activeSnakeHeaderField;
        private Field _nextField;
        private Field _foodField;
        private int _lenght;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private readonly MoveCalculator _moveCalculator;
        private readonly IPauseProcessor _pauseProcessor;
        private List<Field> _snake = new List<Field>();
        public event EventHandler<EventArgs> GameOver; 
        
        public Direction CurrentDirection { get; set; }

        public UpdateableProperty<int> Score = new UpdateableProperty<int>();

        public SnakeLogic(MoveCalculator moveCalculator, IPauseProcessor pauseProcessor)
        {
            _moveCalculator = moveCalculator;
            _pauseProcessor = pauseProcessor;
        }

        public Dictionary<int, List<Field>> Rows { get; } = new Dictionary<int, List<Field>>();

        public void Initialize(int length)
        {

            CurrentDirection = Direction.Right;
            _lenght = length;
            Score.Value = 0;
            
            for (int row = 1; row <= length; row++)
            {
                for (int column = 1; column <= length; column++)
                {
                    Field field = new Field();
                    field.Initialize(row, column);
                    field.Content = FieldContent.Empty;
                    if (!Rows.ContainsKey(row))
                        Rows[row] = new List<Field>();
                    
                    Rows[row].Add(field);
                }
            }
        }
        
        public async Task Start()
        {
            _snake.Add(Rows[1].First());
            _snake.Add(Rows[1].First());
            _snake.Add(Rows[1].First());
            _activeSnakeHeaderField = Rows[1].First();
            _activeSnakeHeaderField.Content = FieldContent.Snake;
            await RefreshFoodField();
            
            while (Move())
            {
                await Task.Delay(150);
            }
            
            GameOver?.Invoke(this, EventArgs.Empty);
        }
        
        private bool Move()
        {
            if (_pauseProcessor.IsPaused)
                return true;
            
            _activeSnakeHeaderField.Content = FieldContent.Empty;
            Field lastFieldOfOldSnakePositions = _snake[_snake.Count-1];
            _snake = _moveCalculator.CalculateNextField(_snake).ToList();
            _nextField = _snake.First();
            MoveResult moveResult = EnterField();

            return ProcessMoveResult(moveResult, lastFieldOfOldSnakePositions);
        }

        private bool ProcessMoveResult(MoveResult moveResult, Field lastFieldOfOldSnakePositions)
        {
            switch (moveResult)
            {
                case MoveResult.Valid:
                {
                    lastFieldOfOldSnakePositions.Content = FieldContent.Empty;
                    _activeSnakeHeaderField = _nextField;
                    foreach (Field field in _snake)
                    {
                        field.Content = FieldContent.Snake;
                    }

                    return true;
                }
                case MoveResult.Overlap:
                    return false;
                case MoveResult.Food:
                {
                    lastFieldOfOldSnakePositions.Content = FieldContent.Empty;
                    _activeSnakeHeaderField.Content = FieldContent.Empty;
                    _activeSnakeHeaderField = _nextField;
                    _activeSnakeHeaderField.Content = FieldContent.Snake;
                    Score.Value++;
                    _snake.Add(_snake.Last());
                    foreach (Field field in _snake)
                    {
                        field.Content = FieldContent.Snake;
                    }

                    RefreshFoodField().FireAndForgetAsync(null);
                    return true;
                }
                default:
                    throw new InvalidEnumArgumentException(nameof(moveResult), (int) moveResult,
                        typeof(MoveResult));
            }
        }

        private MoveResult EnterField()
        {
            switch (_nextField.Content)
            {
                case FieldContent.Fruit:
                    return MoveResult.Food;
                case FieldContent.Snake:
                    return MoveResult.Overlap;
                case FieldContent.Empty:
                    return MoveResult.Valid;
                default:
                    throw new InvalidEnumArgumentException(nameof(_nextField.Content), (int) _nextField.Content,
                        typeof(FieldContent));
            }
        }

        private async Task RefreshFoodField()
        {
            await Task.Delay(1000);
            
            int foodX = _random.Next(1, _lenght);
            int foodY = _random.Next(1,_lenght);
            
            if(foodX == _activeSnakeHeaderField.Row && foodY == _activeSnakeHeaderField.Column)
            {
                await RefreshFoodField();
                return;
            }
            
            _foodField = Rows[foodX].Find(b=>b.Column == foodY);
            _foodField.Content = FieldContent.Fruit;
        }
    }
}