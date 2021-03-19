using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Moody.Snake.Model
{
    internal class MoveCalculator
    {
        private readonly Lazy<SnakeLogic> _snakeLogic;

        public MoveCalculator(Lazy<SnakeLogic> snakeLogic)
        {
            _snakeLogic = snakeLogic;
        }

        public IEnumerable<Field> CalculateNextField(IList<Field> snake)
        {
            Field lastField = null;
            List<Field> nextSnakePositions = new List<Field>();

            int count = 0;
            foreach (Field field in snake)
            {
                try
                {
                    if (lastField == null)
                    {
                        Field nextField;
                        switch (_snakeLogic.Value.CurrentDirection)
                        {
                            case Direction.Down:
                                nextField = MoveDown(field);
                                break;
                            case Direction.Left:
                                nextField = MoveLeft(field);
                                break;
                            case Direction.Right:
                                nextField =  MoveRight(field);
                                break;
                            case Direction.Up:
                                nextField = MoveUp(field);
                                break;
                            default:
                                throw new InvalidEnumArgumentException(nameof(_snakeLogic.Value.CurrentDirection), (int) _snakeLogic.Value.CurrentDirection,
                                    _snakeLogic.Value.CurrentDirection.GetType());
                        }

                        nextSnakePositions.Add(nextField);
                        continue;
                    }

                    Field predecessorField = snake[count-1];
                    if(field.Column == predecessorField.Column && field.Row == predecessorField.Row)
                    {
                        nextSnakePositions.Add(field);
                        continue;
                    }

                    nextSnakePositions.Add(lastField);
                }
                finally
                {
                    count++;
                    lastField = field;
                }
            }

            return nextSnakePositions;
        }
        
        private Field MoveUp(Field currentField)
        {
            return
                _snakeLogic.Value.Rows.ContainsKey(currentField.Row -1)
                    ? _snakeLogic.Value.Rows[currentField.Row -1].First(b => b.Column == currentField.Column)
                    : _snakeLogic.Value.Rows[_snakeLogic.Value.Rows.Count].First(b => b.Column == currentField.Column);
        }
        
        private Field MoveDown(Field currentField)
        {
            return
                _snakeLogic.Value.Rows.ContainsKey(currentField.Row + 1)
                    ? _snakeLogic.Value.Rows[currentField.Row + 1].First(b => b.Column == currentField.Column)
                    : _snakeLogic.Value.Rows[1].First(b => b.Column == currentField.Column);
        }
        
        private Field MoveRight(Field currentField)
        {
            return
                _snakeLogic.Value.Rows[currentField.Row].FirstOrDefault(x => x.Column == currentField.Column + 1) ??
                _snakeLogic.Value.Rows[currentField.Row][0];
        }

        private Field MoveLeft(Field currentField)
        {
            return
                _snakeLogic.Value.Rows[currentField.Row].FirstOrDefault(x => x.Column == currentField.Column - 1) ??
                _snakeLogic.Value.Rows[currentField.Row][_snakeLogic.Value.Rows.Count-1];
        }
    }
}