using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Moody.Snake.Behavior
{
    public class CloseWindowBehavior : Behavior<Window>
    {
        private static Window _parent;
        
        protected override void OnAttached()
        {
            
            base.OnAttached();
            if (!(AssociatedObject is Window window))
                return;

            _parent = window;

            CloseWindow = OnCloseWindowExecute;

        }

        private void OnCloseWindowExecute()
        {
            try
            {
                _parent.Close();
            }
            catch (Exception e)
            {
                //TODO
            }
        }

        public static readonly DependencyProperty CloseWindowProperty = DependencyProperty.Register(
            "CloseWindow", typeof(Action), typeof(CloseWindowBehavior), new PropertyMetadata(default(Action)));

        public Action CloseWindow
        {
            get { return (Action) GetValue(CloseWindowProperty); }
            set { SetValue(CloseWindowProperty, value); }
        }
    }
    
    
}