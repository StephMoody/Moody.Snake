using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moody.Common.Base;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels
{
    internal class GameViewViewModel : ViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
        private readonly SnakeLogic _snakeLogic;
        private readonly GameHeaderViewModel _gameHeaderViewModel;
         
        public GameViewViewModel(ILogManager logManager, Func<RowViewModel> rowViewModelCreator, SnakeLogic snakeLogic, GameHeaderViewModel gameHeaderViewModel) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
            _snakeLogic = snakeLogic;
            _gameHeaderViewModel = gameHeaderViewModel;
        }

        public ObservableCollection<RowViewModel> RowViewModels { get; } = new ObservableCollection<RowViewModel>();
        
        public GameHeaderViewModel GameHeaderViewModel => _gameHeaderViewModel;

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
    }
}