using System;
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
        
        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -=AssociatedObjectOnKeyDown;
            base.OnDetaching();
        }

        public static readonly DependencyProperty OnKeyDownActionProperty = DependencyProperty.Register(
            "OnKeyDownAction", typeof(Action<Key>), typeof(WindowsKeyDown), new PropertyMetadata(default(Action<Key>)));

        public Action<Key> OnKeyDownAction
        {
            get { return (Action<Key>) GetValue(OnKeyDownActionProperty); }
            set { SetValue(OnKeyDownActionProperty, value); }
        }
        
        private void AssociatedObjectOnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                OnKeyDownAction(e.Key);
            }
            catch (Exception exception)
            {
                //
            }
        }
        
    }
}