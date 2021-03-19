using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.ViewModels.Game;

namespace Moody.Snake.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly GameViewViewModel _gameViewViewModel;
        private readonly StartViewViewModel _startViewViewModel;
        private readonly PauseViewModel _pauseViewModel;
        private readonly IActiveMode _activeMode;
        private ViewModelBase _contentViewModel;
        private Action<Key> _onKeyDownAction;

        public MainWindowViewModel([NotNull] ILogManager logManager,
            [NotNull] GameViewViewModel gameViewViewModel,
            [NotNull] StartViewViewModel startViewViewModel,
            IActiveMode activeMode,
            PauseViewModel pauseViewModel) : base(logManager)
        {
            _gameViewViewModel = gameViewViewModel;
            _startViewViewModel = startViewViewModel;
            _activeMode = activeMode;
            _pauseViewModel = pauseViewModel;
        }

        public override Task Initialize()
        {
            _gameViewViewModel.Initialize();
            _pauseViewModel.Initialize();
            ContentViewModel = _startViewViewModel;
            OnKeyDownAction = ExecuteOnKeyDown;
            _activeMode.ModeChanged += ActiveModeOnModeChanged;
            return base.Initialize();
        }

        private void ActiveModeOnModeChanged(object sender, EventArgs e)
        {
            if (_activeMode.Value == Mode.Game)
                ContentViewModel = GameViewViewModel;

            if (_activeMode.Value == Mode.Pause)
                ContentViewModel = _pauseViewModel;
        }

        private void ExecuteOnKeyDown(Key key)
        {
            try
            {
                switch (_activeMode.Value)
                {
                    case Mode.Menu:
                        _activeMode.SetValue(Mode.Game);
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