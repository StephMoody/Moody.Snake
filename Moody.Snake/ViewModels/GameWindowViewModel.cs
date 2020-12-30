using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels
{
    internal class GameWindowViewModel : ViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
        private readonly SnakeLogic _snakeLogic;
         
        public GameWindowViewModel(ILogManager logManager, Func<RowViewModel> rowViewModelCreator, SnakeLogic snakeLogic) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
            _snakeLogic = snakeLogic;
        }

        public ObservableCollection<RowViewModel> RowViewModels { get; } = new ObservableCollection<RowViewModel>();
        
        public override Task Initialize()
        {
            foreach (KeyValuePair<int, List<Field>> snakeLogicRow in _snakeLogic.Rows)
            {
                RowViewModel newRowViewModel = _rowViewModelCreator.Invoke();
                newRowViewModel.Initialize(snakeLogicRow.Key, snakeLogicRow.Value);
                RowViewModels.Add(newRowViewModel);
            }
            
            OnPropertyChanged(nameof(RowViewModels));
            return Task.CompletedTask;
        }
    }
}