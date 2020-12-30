using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels
{
    internal class RowViewModel : ViewModelBase
    {
        private readonly Func<Field, FieldViewModel> _fieldViewModelCreator;
        
        public RowViewModel(ILogManager logManager, Func<Field, FieldViewModel> fieldViewModelCreator) : base(logManager)
        {
            _fieldViewModelCreator = fieldViewModelCreator;
        }

        public ObservableCollection<FieldViewModel> FieldViewModels { get; } = new ObservableCollection<FieldViewModel>();

        public int RowNumber { get; private set; }

        public void Initialize(int rowNumber, List<Field> fields)
        {
            RowNumber = rowNumber;

            foreach (Field field in fields)
            {
                FieldViewModel newField = _fieldViewModelCreator.Invoke(field);
                FieldViewModels.Add(newField);
            }
        }
    }
}