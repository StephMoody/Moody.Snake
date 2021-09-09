using System.Windows;
using Moody.MVVM.Base.ViewModel;
using Moody.UI.Contracts;

namespace Moody.UI.WindowHandling
{
    internal class WindowShower<TWindow, TViewModel> : IWindowShower<TWindow,TViewModel> where TViewModel : ViewModelBase where TWindow : Window
    {
        private readonly TWindow _window;
        private readonly TViewModel _viewModelBase;

        public WindowShower(TWindow window, TViewModel viewModelBase)
        {
            _window = window;
            _viewModelBase = viewModelBase;
        }

        public void Show()
        {
            _window.DataContext = _viewModelBase;
            _window.Show();
        }

        public TViewModel ViewModel => _viewModelBase;

        public void Close()
        {
            _window.Close();
        }
    }
}