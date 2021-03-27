using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;
using Moody.Snake.ViewModels.Game;
using Moody.Snake.ViewModels.Mode;

namespace Moody.Snake.ViewModels.Content
{
    internal class GameViewViewModel : ContentViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
        private readonly SnakeLogic _snakeLogic;
        private readonly GameHeaderViewModel _gameHeaderViewModel;
        private Action _onCloseWindow;
        private readonly IActiveMode _activeMode;

        public GameViewViewModel(ILogManager logManager,
            Func<RowViewModel> rowViewModelCreator,
            SnakeLogic snakeLogic,
            GameHeaderViewModel gameHeaderViewModel,
            PauseViewModel pauseViewModel, 
            IActiveMode activeMode) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
            _snakeLogic = snakeLogic;
            _gameHeaderViewModel = gameHeaderViewModel;
            PauseViewModel = pauseViewModel;
            _activeMode = activeMode;
        }

        public PauseViewModel PauseViewModel { get; }

        public ObservableCollection<RowViewModel> RowViewModels { get; } = new ObservableCollection<RowViewModel>();
        
        public GameHeaderViewModel GameHeaderViewModel => _gameHeaderViewModel;
        
        public Action OnCloseWindow
        {
            private get => _onCloseWindow;
            set => _onCloseWindow = value;
        }

        public Direction CurrentDirection
        {
            get => _snakeLogic.CurrentDirection;
            set
            {
                _snakeLogic.CurrentDirection = value;
                OnPropertyChanged();
            }

        }
        
        public override Task Initialize()
        {
            foreach (KeyValuePair<int, List<Field>> snakeLogicRow in _snakeLogic.Rows)
            {
                RowViewModel newRowViewModel = _rowViewModelCreator.Invoke();
                newRowViewModel.Initialize(snakeLogicRow.Key, snakeLogicRow.Value);
                RowViewModels.Add(newRowViewModel);
            }

            _gameHeaderViewModel.Initialize();
            
            OnPropertyChanged(nameof(RowViewModels));
            return Task.CompletedTask;
        }

        
        public override void HandleKeyDown(Key key)
        {
            switch (key)
            {
                case Key.P:
                    _activeMode.SetValue(IsPaused ? Mode.ContentModes.Game : Mode.ContentModes.Pause);
                    break;
                case Key.Escape:
                    _onCloseWindow();
                    return;
                case Key.Up:
                    CurrentDirection = Direction.Up;
                    break;
                case Key.Down:
                    CurrentDirection = Direction.Down;
                    break;
                case Key.Left:
                    CurrentDirection = Direction.Left;
                    break;
                case Key.Right:
                    CurrentDirection = Direction.Right;
                    break;
            }
        }
        
        private bool IsPaused => _activeMode.Value == Mode.ContentModes.Pause;

    }
}