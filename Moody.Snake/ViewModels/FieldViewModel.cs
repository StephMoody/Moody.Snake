using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels
{
    internal class FieldViewModel : ViewModelBase
    {
        private FieldContent _content;

        public FieldViewModel(ILogManager logManager) : base(logManager)
        {
        }

        public void Initialize(int x, int y)
        {
            PositionX = x;
            PositionY = y;
            Content = FieldContent.Empty;
        }
        
        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public FieldContent Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
    }
}