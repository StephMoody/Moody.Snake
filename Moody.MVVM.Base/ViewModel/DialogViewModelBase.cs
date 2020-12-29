using System;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.Command;

namespace Moody.MVVM.Base.ViewModel
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        private readonly Func<Task> _onAccept;

        public DialogViewModelBase(ILogManager logManager, Func<Task> onAccept) : base(logManager)
        {
            _onAccept = onAccept;
            AcceptCommand = new AsyncCommand(logManager,  CheckCanExecute, OnAccept);
        }

        protected virtual async Task OnAccept()
        {
            await _onAccept();
        }

        public AsyncCommand AcceptCommand { get; }
        protected abstract bool CheckCanExecute();
    }
}