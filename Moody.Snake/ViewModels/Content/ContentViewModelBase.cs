using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels.Content
{
    internal abstract class ContentViewModelBase : ViewModelBase
    {
        public ContentViewModelBase(ILogManager logManager) : base(logManager)
        {
        }

        public abstract void HandleKeyDown(Key key);

    }
}