using System;
using System.Threading.Tasks;
using Moody.Common.Base;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels
{
    internal class GameHeaderViewModel : ViewModelBase, IDisposable
    {
        private readonly SnakeLogic _snakeLogic;

        public GameHeaderViewModel(ILogManager logManager, SnakeLogic snakeLogic) : base(logManager)
        {
            _snakeLogic = snakeLogic;
        }

        public override Task Initialize()
        {
            _snakeLogic.Score.ValueUpdated += ScoreOnValueUpdated;
            return base.Initialize();
        }

        private void ScoreOnValueUpdated(object sender, ValueChangedEventArgs<int> e)
        {
            try
            {
                OnPropertyChanged(nameof(CurrentScore));
            }
            catch (Exception exception)
            {
                LogManager.Error(exception);
            }
        }

        public int CurrentScore => _snakeLogic.Score.Value;

        public void Dispose()
        {
            _snakeLogic.Score.ValueUpdated -= ScoreOnValueUpdated;
        }
    }

}