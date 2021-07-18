using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Moody.Common.Base;
using Moody.Common.Extensions;

namespace Moody.Snake.Model.Game
{
    internal class MoveProcessor
    {
        
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private readonly MoveCalculator _moveCalculator;
        private readonly IPauseProcessor _pauseProcessor;
        private readonly IGameField _gameField;
        private readonly ISnake _snake;
        private readonly FieldState _fieldState = new FieldState();
        
        public event EventHandler<EventArgs> GameOver; 
        
        public Direction CurrentDirection { get; set; }

        public readonly UpdateableProperty<int> Score = new UpdateableProperty<int>();

        public MoveProcessor(MoveCalculator moveCalculator,
            IPauseProcessor pauseProcessor,
            IGameField gameField,
            ISnake snake)
        {
            _moveCalculator = moveCalculator;
            _pauseProcessor = pauseProcessor;
            _gameField = gameField;
            _snake = snake;
        }
        
        public void Initialize(int length)
        {
            _gameField.Initialize(length);
            _snake.Initialize();
            CurrentDirection = Direction.Right;
            Score.Value = 0;
        }
        
        public async Task Start()
        {
            _fieldState.ActiveSnakeHeaderField = _gameField.Rows[1].First();
            _fieldState.ActiveSnakeHeaderField.Content = FieldContent.Snake;
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
            
            _fieldState.ActiveSnakeHeaderField.Content = FieldContent.Empty;
            Field lastFieldOfOldSnakePositions = _snake.Fields.Last();
            List<Field> nextPositionFields = _moveCalculator.CalculateNextPositions().ToList();
            _snake.ApplyPosition(nextPositionFields);
            
            _fieldState.NextField = nextPositionFields.First();
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
                    _fieldState.ActiveSnakeHeaderField = _fieldState.NextField;
                    foreach (Field field in _snake.Fields)
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
                    _fieldState.ActiveSnakeHeaderField.Content = FieldContent.Empty;
                    _fieldState.ActiveSnakeHeaderField = _fieldState.NextField;
                    _fieldState.ActiveSnakeHeaderField.Content = FieldContent.Snake;
                    Score.Value++;
                    _snake.Grow(_snake.Fields.Last());
                    foreach (Field field in _snake.Fields)
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
            switch (_fieldState.NextField.Content)
            {
                case FieldContent.Fruit:
                    return MoveResult.Food;
                case FieldContent.Snake:
                    return MoveResult.Overlap;
                case FieldContent.Empty:
                    return MoveResult.Valid;
                default:
                    throw new InvalidEnumArgumentException(nameof(_fieldState.NextField.Content), (int) _fieldState.NextField.Content,
                        typeof(FieldContent));
            }
        }

        private async Task RefreshFoodField()
        {
            await Task.Delay(1000);
            
            int foodX = _random.Next(1, _gameField.Lenght);
            int foodY = _random.Next(1,_gameField.Lenght);
            
            if(foodX == _fieldState.ActiveSnakeHeaderField.Row && foodY == _fieldState.ActiveSnakeHeaderField.Column)
            {
                await RefreshFoodField();
                return;
            }
            
            _fieldState.FoodField = _gameField.Rows[foodX].Find(b=>b.Column == foodY);
            _fieldState.FoodField.Content = FieldContent.Fruit;
        }
    }
}