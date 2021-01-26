using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Moody.Snake.Model;

namespace Moody.Snake.Behavior
{
    public class WindowsKeyDown : Behavior<Window>
    {
        protected override void OnAttached()
        {
            
            AssociatedObject.KeyDown +=AssociatedObjectOnKeyDown;
            base.OnAttached();
        }
        
        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -=AssociatedObjectOnKeyDown;
            base.OnDetaching();
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction", typeof(Direction), typeof(WindowsKeyDown), new PropertyMetadata(default(Direction)));

        public Direction Direction
        {
            get { return (Direction) GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }
        
        private void AssociatedObjectOnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    Direction = Direction.Up;
                    break;
                case Key.Down:
                    Direction = Direction.Down;
                    break;
                case Key.Left:
                    Direction = Direction.Left;
                    break;
                case Key.Right:
                    Direction = Direction.Right;
                    break;
            }
        }
    }
}