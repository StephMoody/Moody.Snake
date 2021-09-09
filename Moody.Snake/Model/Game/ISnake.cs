using System.Collections.Generic;

namespace Moody.Snake.Model.Game
{
    internal interface ISnake
    {
        void Grow(Field field);

        void Initialize();
        IReadOnlyList<Field> Fields { get; }
        void ApplyPosition(List<Field> nextPositionFields);
    }
}