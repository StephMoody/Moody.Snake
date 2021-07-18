using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Moody.Common.Base;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.ViewModels.Content;
using Moody.Snake.ViewModels.Mode;

namespace Moody.Snake.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        [NotNull] private readonly GameViewViewModel _gameViewViewModel;
        [NotNull] private readonly StartViewViewModel _startViewViewModel;
        [NotNull] private readonly PauseViewModel _pauseViewModel;
        [NotNull] private readonly GameOverViewViewModel _gameOverViewViewModel;
        [NotNull] private readonly IActiveMode _activeMode;
        private ContentViewModelBase _contentViewModel;
        private Action<Key> _onKeyDownAction;
        private Action _onCloseWindow; 

        public MainWindowViewModel([NotNull] ILogManager logManager,
            [NotNull] GameViewViewModel gameViewViewModel,
            [NotNull] StartViewViewModel startViewViewModel,
            IActiveMode activeMode,
            [NotNull] PauseViewModel pauseViewModel,
            [NotNull] GameOverViewViewModel gameOverViewViewModel) : base(logManager)
        {
            _gameViewViewModel = gameViewViewModel;
            _startViewViewModel = startViewViewModel;
            _activeMode = activeMode;
            _pauseViewModel = pauseViewModel;
            _gameOverViewViewModel = gameOverViewViewModel;
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

        private void ActiveModeOnModeChanged(object sender, ValueChangedEventArgs<ContentModes> valueChangedEventArgs)
        {
            if (valueChangedEventArgs.NewValue == ContentModes.Game)
                ContentViewModel = GameViewViewModel;

            if (valueChangedEventArgs.NewValue == ContentModes.Pause)
                ContentViewModel = _pauseViewModel;

            if (valueChangedEventArgs.NewValue == ContentModes.GameOver)
                ContentViewModel = _gameOverViewViewModel;
            
        }

        private void ExecuteOnKeyDown(Key key)
        {
            try
            {
                if (key == Key.Escape)
                {
                    _onCloseWindow();
                    return;
                }
                
                ContentViewModel.HandleKeyDown(key);
            }
            catch (Exception e)
            {
                LogManager.Error(e);
            }
        }

        public ContentViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            set
            {
                _contentViewModel = value;
                OnPropertyChanged();
            }
        }

        public GameViewViewModel GameViewViewModel => _gameViewViewModel;
        
        public Action OnCloseWindow
        {
            private get => _onCloseWindow;
            set => _onCloseWindow = value;
        }

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