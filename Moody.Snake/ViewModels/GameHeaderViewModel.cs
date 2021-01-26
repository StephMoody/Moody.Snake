using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels
{
    public class GameHeaderViewModel : ViewModelBase
    {
        private int _currentScore;

        public GameHeaderViewModel(ILogManager logManager) : base(logManager)
        {
        }

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                OnPropertyChanged();
            }
        }

        public void Score()
        {
            CurrentScore++;
        }
    }
}