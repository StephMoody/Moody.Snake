﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.Common.Extensions;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels.Game
{
    internal class GameViewViewModel : ViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
        private readonly SnakeLogic _snakeLogic;
        private readonly GameHeaderViewModel _gameHeaderViewModel;
        private Action _onCloseWindow;

        public GameViewViewModel(ILogManager logManager, Func<RowViewModel> rowViewModelCreator, SnakeLogic snakeLogic, GameHeaderViewModel gameHeaderViewModel, PauseViewModel pauseViewModel) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
            _snakeLogic = snakeLogic;
            _gameHeaderViewModel = gameHeaderViewModel;
            PauseViewModel = pauseViewModel;
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

        public bool IsPaused => _snakeLogic.IsPaused;

        public void HandleKeyDown(Key key)
        {
            switch (key)
            {
                case Key.P:
                    ExecutePauseHandling();
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

        private void ExecutePauseHandling()
        {
            _snakeLogic.IsPaused = !_snakeLogic.IsPaused;
            if(_snakeLogic.IsPaused)
                PauseViewModel.StartPauseFeed().FireAndForgetAsync(LogManager);
            else
            {
                PauseViewModel.EndPauseFeed();
            }
            OnPropertyChanged(nameof(IsPaused));
        }
    }
}