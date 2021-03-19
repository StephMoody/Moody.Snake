using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model.News;

namespace Moody.Snake.ViewModels.Game
{
    public class PauseViewModel : ViewModelBase

    {
        private string _newsHeader;
        private string _newsMessage;
        private INewsFeed _newsFeed;
        private bool _feedIsActive = false;

        public PauseViewModel(ILogManager logManager, INewsFeed newsFeed) : base(logManager)
        {
            _newsFeed = newsFeed;
        }

        public void EndPauseFeed()
        {
            _feedIsActive = false;
        }
        
        public async Task StartPauseFeed()
        {
            if (_feedIsActive)
                return;
            
            _feedIsActive = true;
            while (_feedIsActive)
            {
                foreach (NewsItem newsItem in _newsFeed.News)
                {
                    NewsHeader = newsItem.Header;
                    NewsMessage = newsItem.Message;
                    await Task.Delay(5000);
                }
            }
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
    }
}