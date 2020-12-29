using System;
using System.Collections.ObjectModel;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels
{
    internal class RowViewModel : ViewModelBase
    {
        private readonly Func<FieldViewModel> _fieldViewModelCreator;
        
        public RowViewModel(ILogManager logManager, Func<FieldViewModel> fieldViewModelCreator) : base(logManager)
        {
            _fieldViewModelCreator = fieldViewModelCreator;
        }

        public ObservableCollection<FieldViewModel> FieldViewModels { get; } = new ObservableCollection<FieldViewModel>();

        public int RowNumber { get; private set; }

        public void Initialize(int lenght, int rowNumber)
        {
            RowNumber = rowNumber;
            
            for (int i = 1; i <= lenght; i++)
            {
                FieldViewModel newField = _fieldViewModelCreator.Invoke();
                newField.Initialize(i, rowNumber);
                FieldViewModels.Add(newField);
            }
        }
        
    }
}