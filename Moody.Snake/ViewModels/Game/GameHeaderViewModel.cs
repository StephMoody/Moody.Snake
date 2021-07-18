using System;
using System.Threading.Tasks;
using Moody.Common.Base;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;
using Moody.Snake.Model.Game;

namespace Moody.Snake.ViewModels.Game
{
    internal class GameHeaderViewModel : ViewModelBase, IDisposable
    {
        private readonly MoveProcessor _moveProcessor;

        public GameHeaderViewModel(ILogManager logManager, MoveProcessor moveProcessor) : base(logManager)
        {
            _moveProcessor = moveProcessor;
        }

        public override Task Initialize()
        {
            _moveProcessor.Score.ValueUpdated += ScoreOnValueUpdated;
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

        public int CurrentScore => _moveProcessor.Score.Value;

        public void Dispose()
        {
            _moveProcessor.Score.ValueUpdated -= ScoreOnValueUpdated;
        }
    }

}