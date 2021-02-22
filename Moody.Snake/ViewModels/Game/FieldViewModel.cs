using System;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels.Game
{
    internal class FieldViewModel : ViewModelBase, IDisposable
    {
        private readonly Field _field;

        public FieldViewModel(ILogManager logManager, Field field) : base(logManager)
        {
            _field = field;
            _field.ContentUpdated += FieldOnContentUpdated;
            
        }

        public int PositionX => _field.Row;

        public int PositionY => _field.Column;

        public FieldContent Content => _field.Content;
        
        private void FieldOnContentUpdated(object sender, EventArgs e)
        {
            try
            {
                OnPropertyChanged(nameof(Content));
            }
            catch (Exception exception)
            {
                LogManager.Error(exception);
            }
        }


        public void Dispose()
        {
            _field.ContentUpdated -= FieldOnContentUpdated;

        }
    }
}