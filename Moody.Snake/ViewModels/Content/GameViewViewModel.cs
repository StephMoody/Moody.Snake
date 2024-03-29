﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.Snake.Model;
using Moody.Snake.Model.Game;
using Moody.Snake.ViewModels.Game;
using Moody.Snake.ViewModels.Mode;

namespace Moody.Snake.ViewModels.Content
{
    internal class GameViewViewModel : ContentViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
        private readonly MoveProcessor _moveProcessor;
        private readonly GameHeaderViewModel _gameHeaderViewModel;
        private readonly IActiveMode _activeMode;
        private readonly IGameField _gameField;

        public GameViewViewModel(ILogManager logManager,
            Func<RowViewModel> rowViewModelCreator,
            MoveProcessor moveProcessor,
            GameHeaderViewModel gameHeaderViewModel,
            PauseViewModel pauseViewModel, 
            IActiveMode activeMode, IGameField gameField) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
            _moveProcessor = moveProcessor;
            _gameHeaderViewModel = gameHeaderViewModel;
            PauseViewModel = pauseViewModel;
            _activeMode = activeMode;
            _gameField = gameField;
        }

        public PauseViewModel PauseViewModel { get; }

        public ObservableCollection<RowViewModel> RowViewModels { get; } = new ObservableCollection<RowViewModel>();
        
        public GameHeaderViewModel GameHeaderViewModel => _gameHeaderViewModel;
        
        public Direction CurrentDirection
        {
            get => _moveProcessor.CurrentDirection;
            set
            {
                _moveProcessor.CurrentDirection = value;
                OnPropertyChanged();
            }

        }
        
        public override Task Initialize()
        {
            foreach (KeyValuePair<int, List<Field>> snakeLogicRow in _gameField.Rows)
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
                    _activeMode.SetValue(IsPaused ? ContentModes.Game : ContentModes.Pause);
                    break;
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