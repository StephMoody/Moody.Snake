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
        private Field _activeField;
        private Field _nextField;
        private Field _foodField;
        private int _lenght;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private bool _isPaused;
        
        public Direction CurrentDirection { get; set; }

        public UpdateableProperty<int> Score = new UpdateableProperty<int>();

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
            _activeField = Rows[1].First();
            _activeField.Content = FieldContent.Snake;
            await RefreshFoodField();
            
            while (Move())
            {
                await Task.Delay(150);
            }
        }

        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        private bool Move()
        {
            if (_isPaused)
                return true;
            
            _activeField.Content = FieldContent.Empty;
            MoveResult moveResult;

            switch (CurrentDirection)
            {
                case Direction.Down:
                    moveResult = MoveDown();
                    break;
                case Direction.Left:
                    moveResult = MoveLeft();
                    break;
                case Direction.Right:
                    moveResult =  MoveRight();
                    break;
                case Direction.Up:
                    moveResult = MoveUp();
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(CurrentDirection), (int) CurrentDirection,
                        CurrentDirection.GetType());
            }

            switch (moveResult)
            {
                case MoveResult.Valid:
                {
                    _activeField.Content = FieldContent.Empty;
                    _activeField = _nextField;
                    _activeField.Content = FieldContent.Snake;
                    return true;
                }
                case MoveResult.Overlap:
                    return false;
                case MoveResult.Food:
                {
                    _activeField.Content = FieldContent.Empty;
                    _activeField = _nextField;
                    _activeField.Content = FieldContent.Snake;
                    Score.Value++;
                    RefreshFoodField().FireAndForgetAsync(null);
                    return true;
                }
                default:
                    throw new InvalidEnumArgumentException(nameof(moveResult), (int) moveResult,
                        typeof(MoveResult));
            }
        }

        private MoveResult MoveUp()
        {
            _nextField =
                Rows.ContainsKey(_activeField.Row -1)
                    ? Rows[_activeField.Row -1].First(b => b.Column == _activeField.Column)
                    : Rows[_lenght].First(b => b.Column == _activeField.Column);
            return EnterField();
        }
        
        private MoveResult MoveDown()
        {
            _nextField =
                Rows.ContainsKey(_activeField.Row + 1)
                    ? Rows[_activeField.Row + 1].First(b => b.Column == _activeField.Column)
                    : Rows[1].First(b => b.Column == _activeField.Column);
            return EnterField();
        }
        
        private MoveResult MoveRight()
        {
            _nextField =
                Rows[_activeField.Row].FirstOrDefault(x => x.Column == _activeField.Column + 1) ??
                Rows[_activeField.Row][0];
            return EnterField();
        }

        private MoveResult MoveLeft()
        {
            _nextField =
                Rows[_activeField.Row].FirstOrDefault(x => x.Column == _activeField.Column - 1) ??
                Rows[_activeField.Row][_lenght-1];
            return EnterField();
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
            
            if(foodX == _activeField.Row && foodY == _activeField.Column)
            {
                await RefreshFoodField();
                return;
            }
            
            _foodField = Rows[foodX].Find(b=>b.Column == foodY);
            _foodField.Content = FieldContent.Fruit;
        }
    }
}