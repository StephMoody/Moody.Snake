using System;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels.Game
{
    internal class PauseViewModel : ViewModelBase
    {
        private string _newsHeader;
        private string _newsMessage;
        private readonly IPauseProcessor _pauseProcessor;

        public PauseViewModel(ILogManager logManager, IPauseProcessor pauseProcessor) : base(logManager)
        {
            _pauseProcessor = pauseProcessor;
        }

        public override Task Initialize()
        {
            _pauseProcessor.NewsUpdated += PauseProcessorOnNewsUpdated;
            return base.Initialize();
        }

    
        public string Source => "Quelle tagesschau.de";

        public string NewsHeader
        {
            get => _newsHeader;
            set
            {
                _newsHeader = value;
                OnPropertyChanged();
            }
        }

        public string NewsMessage
        {
            get => _newsMessage;
            set
            {
                _newsMessage = value; 
                OnPropertyChanged();
            }
        }
        
        private void PauseProcessorOnNewsUpdated(object sender, NewsFeedEventArgs e)
        {
            try
            {
                NewsHeader = e.NewsItem.Header;
                NewsMessage = e.NewsItem.Message;
            }
            catch (Exception exception)
            {
                LogManager.Error(exception);
            }
        }

    }
}