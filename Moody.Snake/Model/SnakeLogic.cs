using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Moody.Snake.Model
{
    internal class SnakeLogic
    {
        private Field _activeField;
        private int _lenght;
        
        public Direction CurrentDirection { get; set; }

        public Dictionary<int, List<Field>> Rows { get; } = new Dictionary<int, List<Field>>();

        public void Initialize(int length)
        {

            CurrentDirection = Direction.Right;
            _lenght = length;
            
            for (int row = 1; row <= length; row++)
            {
                for (int column = 1; column < length; column++)
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

            await Task.Delay(1000);
            
            while (Move())
            {
                await Task.Delay(1000);
            }
        }

        private bool Move()
        {
            _activeField.Content = FieldContent.Empty;
            
            var newX = _activeField.PositionX + 1;
            if (_lenght < newX)
                return false;

            switch (CurrentDirection)
            {
                case Direction.Down:
                    return MoveDown();
                case Direction.Left:
                    return  MoveLeft();
                case Direction.Right:
                    return  MoveRight();
                case Direction.Up:
                    return  MoveUp();
                default:
                    throw new InvalidEnumArgumentException(nameof(CurrentDirection), (int) CurrentDirection,
                        CurrentDirection.GetType());
            }
        }

        private bool MoveUp()
        {
            Field nextField =
                Rows.ContainsKey(_activeField.PositionY + 1)
                    ? Rows[_activeField.PositionY + 1].First(b => b.PositionX == _activeField.PositionX)
                    : Rows[_lenght - 1].First(b => b.PositionX == _activeField.PositionX);
            _activeField.Content = FieldContent.Empty;
            _activeField = nextField;
            _activeField.Content = FieldContent.Snake;

            return true;
        }
        
        private bool MoveDown()
        {
            Field nextField =
                Rows.ContainsKey(_activeField.PositionY + 1)
                    ? Rows[_activeField.PositionY + 1].First(b => b.PositionX == _activeField.PositionX)
                    : Rows[_lenght - 1].First(b => b.PositionX == _activeField.PositionX);
            _activeField.Content = FieldContent.Empty;
            _activeField = nextField;
            _activeField.Content = FieldContent.Snake;
            
            return true;
        }
        
        private bool MoveRight()
        {
            Field nextField =
                Rows[_activeField.PositionX].FirstOrDefault(x => x.PositionY == _activeField.PositionY + 1) ??
                Rows[_activeField.PositionX][0];
            _activeField.Content = FieldContent.Empty;
            _activeField = nextField;
            _activeField.Content = FieldContent.Snake;

            return true;
        }

        private bool MoveLeft()
        {
            Field nextField =
                Rows[_activeField.PositionX].FirstOrDefault(x => x.PositionY == _activeField.PositionY - 1) ??
                Rows[_activeField.PositionX][_lenght-1];
            _activeField.Content = FieldContent.Empty;
            _activeField = nextField;
            _activeField.Content = FieldContent.Snake;

            return true;
        }
    }
}