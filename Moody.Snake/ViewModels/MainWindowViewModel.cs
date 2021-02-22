using System;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;
using Moody.Snake.ViewModels.Game;

namespace Moody.Snake.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly GameViewViewModel _gameViewViewModel;
        private readonly SnakeLogic _snakeLogic;
        private readonly StartViewViewModel _startViewViewModel;
        private ViewModelBase _contentViewModel;
        private Mode _currentMode;
        private Action<Key> _onKeyDownAction;

        public MainWindowViewModel([NotNull] ILogManager logManager,
            [NotNull] GameViewViewModel gameViewViewModel,
            [NotNull] StartViewViewModel startViewViewModel,
            [NotNull] SnakeLogic snakeLogic) : base(logManager)
        {
            _gameViewViewModel = gameViewViewModel;
            _startViewViewModel = startViewViewModel;
            _snakeLogic = snakeLogic;
        }

        public override Task Initialize()
        {
            _gameViewViewModel.Initialize();
            ContentViewModel = _startViewViewModel;
            _currentMode = Mode.Menu;
            OnKeyDownAction = ExecuteOnKeyDown;
            return base.Initialize();
        }

        private void ExecuteOnKeyDown(Key key)
        {
            try
            {
                switch (_currentMode)
                {
                    case Mode.Menu:
                        ContentViewModel = _gameViewViewModel;
                        _currentMode = Mode.Game;
                        break;
                    case Mode.Game:
                    case Mode.Pause:
                    {
                        _gameViewViewModel.HandleKeyDown(key);
                        break;
                    }
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                LogManager.Error(e);
            }
        }

        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            set
            {
                _contentViewModel = value;
                OnPropertyChanged();
            }
        }

        public GameViewViewModel GameViewViewModel => _gameViewViewModel;

        public Action<Key> OnKeyDownAction
        {
            get => _onKeyDownAction;
            private set
            {
                _onKeyDownAction = value;
                OnPropertyChanged();
            }
        }
    }
}