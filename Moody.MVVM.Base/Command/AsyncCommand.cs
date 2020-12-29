using System;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.Common.Extensions;
using Moody.MVVM.Base.Contracts;

namespace Moody.MVVM.Base.Command
{
    public class AsyncCommand : IAsyncCommand
    {

        private readonly ILogManager _logManager;
        private readonly Func<Task> _executeTask;
        private readonly Func<bool> _checkCanExecute;
        private bool _canExecute;

        public AsyncCommand(ILogManager logManager, Func<bool> canExecute, Func<Task> executeTask)
        {
            _logManager = logManager;
            _checkCanExecute = canExecute;
            _executeTask = executeTask;
        }

        public bool CanExecute(object parameter)
        {
            bool newValue = _checkCanExecute.Invoke();
            if (newValue == _canExecute) return _canExecute;

            _canExecute = newValue;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _executeTask.Invoke().FireAndForgetAsync(_logManager);
        }

        public event EventHandler CanExecuteChanged;
    }
}