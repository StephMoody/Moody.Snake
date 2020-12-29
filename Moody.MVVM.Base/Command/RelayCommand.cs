using System;
using System.Windows.Input;
using Moody.Common.Contracts;

namespace Moody.MVVM.Base.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private bool _canExecute;
        private readonly Func<object, bool> _checkCanExecute;

        private readonly ILogManager _logManager;
        private readonly Action _action;

        public RelayCommand(ILogManager logManager,Action action, Func<object, bool> checkCanExecute = null)
        {
            _logManager = logManager;
            _action = action;
            _checkCanExecute = checkCanExecute ?? CanExecuteBase;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                bool newValue = _checkCanExecute.Invoke(parameter);
                if (newValue == _canExecute) return _canExecute;

                _canExecute = newValue;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);

                return _canExecute;
            }
            catch (Exception e)
            {
                _logManager.Error(e);
                return false;
            }
        }

        private bool CanExecuteBase(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                _action();
            }
            catch (Exception e)
            {
                _logManager.Error(e);
            }
        }
    }
}