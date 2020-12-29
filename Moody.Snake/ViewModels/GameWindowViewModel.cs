using System;
using System.Collections.ObjectModel;
using Moody.Common.Contracts;
using Moody.MVVM.Base.Command;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels
{
    internal class GameWindowViewModel : ViewModelBase
    {
        private readonly Func<RowViewModel> _rowViewModelCreator;
         
        public GameWindowViewModel(ILogManager logManager, Func<RowViewModel> rowViewModelCreator) : base(logManager)
        {
            _rowViewModelCreator = rowViewModelCreator;
        }

        public ObservableCollection<RowViewModel> RowViewModels { get; } = new ObservableCollection<RowViewModel>();

        public RelayCommand KeyDown = new RelayCommand(null, () =>
        {
            Console.WriteLine();
        });
            
        
        public void Initialize(int lenght)
        {
            for (int i = 1; i <= lenght; i++)
            {
                RowViewModel row = _rowViewModelCreator.Invoke();
                row.Initialize(lenght, i);
                RowViewModels.Add(row);
            }
            
            OnPropertyChanged(nameof(RowViewModels));
        }
    }
}