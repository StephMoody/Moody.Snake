using System.Collections.Generic;

namespace Moody.Snake.Model.Game
{
    internal interface IGameField
    {
        Dictionary<int, List<Field>> Rows { get; }
        int Lenght { get; }
        void Initialize(int length);
    }

    internal class GameField : IGameField
    {
        public Dictionary<int, List<Field>> Rows { get; } = new Dictionary<int, List<Field>>();

        public void Initialize(int length)
        {

            Lenght = length;
            
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
        
        public int Lenght { get; private set; }     
    }
}