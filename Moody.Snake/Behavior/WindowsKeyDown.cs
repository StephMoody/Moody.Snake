using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Moody.Snake.Behavior
{
    public class WindowsKeyDown : Behavior<Window>
    {
        protected override void OnAttached()
        {
            
            AssociatedObject.KeyDown +=AssociatedObjectOnKeyDown;
            base.OnAttached();
        }

        private void AssociatedObjectOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                
            }
        }
        
        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -=AssociatedObjectOnKeyDown;
            base.OnDetaching();
        }
    }
}