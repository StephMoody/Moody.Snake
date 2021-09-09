using System.Collections.Generic;

namespace Moody.Snake.Model.Game
{
    internal interface IGameField
    {
        Dictionary<int, List<Field>> Rows { get; }
        int Lenght { get; }
        void Initialize(int length);
    }
}