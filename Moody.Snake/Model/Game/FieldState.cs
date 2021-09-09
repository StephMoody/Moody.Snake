namespace Moody.Snake.Model.Game
{
    internal interface IFieldState
    {
        Field ActiveSnakeHeaderField { get; set; }
        Field NextField { get; set; }
        Field FoodField { get; set; }
    }

    internal class FieldState : IFieldState
    {
        public Field ActiveSnakeHeaderField { get; set; }
        public Field NextField { get; set; }
        public Field FoodField { get; set; }
    }
}