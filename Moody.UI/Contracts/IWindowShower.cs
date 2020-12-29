using System.Windows;
using Moody.MVVM.Base.ViewModel;

namespace Moody.UI.Contracts
{
    public interface IWindowShower<TWindow, TViewModel> where TWindow : Window  where TViewModel : ViewModelBase
    {
        void Show();

        TViewModel ViewModel { get; }
    }
}