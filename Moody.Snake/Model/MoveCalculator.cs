using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Moody.Snake.Model.Game;

namespace Moody.Snake.Model
{
    internal class MoveCalculator
    {
        private readonly Lazy<MoveProcessor> _snakeLogic;
        private readonly ISnake _snake;
        private readonly IGameField _gameField;

        public MoveCalculator(Lazy<MoveProcessor> snakeLogic,
            ISnake snake,
            IGameField gameField)
        {
            _snakeLogic = snakeLogic;
            _snake = snake;
            _gameField = gameField;
        }

        public IEnumerable<Field> CalculateNextPositions()
        {
            Field lastField = null;
            List<Field> nextSnakePositions = new List<Field>();

            int count = 0;
            foreach (Field field in _snake.Fields)
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

                    Field predecessorField = _snake.Fields[count-1];
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
                _gameField.Rows.ContainsKey(currentField.Row -1)
                    ? _gameField.Rows[currentField.Row -1].First(b => b.Column == currentField.Column)
                    : _gameField.Rows[_gameField.Rows.Count].First(b => b.Column == currentField.Column);
        }
        
        private Field MoveDown(Field currentField)
        {
            return
                _gameField.Rows.ContainsKey(currentField.Row + 1)
                    ? _gameField.Rows[currentField.Row + 1].First(b => b.Column == currentField.Column)
                    : _gameField.Rows[1].First(b => b.Column == currentField.Column);
        }
        
        private Field MoveRight(Field currentField)
        {
            return
                _gameField.Rows[currentField.Row].FirstOrDefault(x => x.Column == currentField.Column + 1) ??
                _gameField.Rows[currentField.Row][0];
        }

        private Field MoveLeft(Field currentField)
        {
            return
                _gameField.Rows[currentField.Row].FirstOrDefault(x => x.Column == currentField.Column - 1) ??
                _gameField.Rows[currentField.Row][_gameField.Rows.Count-1];
        }
    }
}