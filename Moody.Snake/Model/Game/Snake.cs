using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Moody.Snake.Model.Game
{
    internal interface ISnake
    {
        void Grow(Field field);

        void Initialize();
        IReadOnlyList<Field> Fields { get; }
        void ApplyPosition(List<Field> nextPositionFields);
    }

    internal class Snake : ISnake
    {
        private readonly List<Field> _snakeFields = new List<Field>();
        private readonly IGameField _gameField;

        public Snake(IGameField gameField)
        {
            _gameField = gameField;
        }

        public IReadOnlyList<Field> Fields => _snakeFields;
        public void ApplyPosition(List<Field> nextPositionFields)
        {
            _snakeFields.Clear();
            _snakeFields.AddRange(nextPositionFields);
        }

        public void Grow(Field field)
        {
            _snakeFields.Add(field);
        }

        public void Initialize()
        {
            Grow(_gameField.Rows[1].First());
            Grow(_gameField.Rows[1].First());
            Grow(_gameField.Rows[1].First());

        }
    }
}