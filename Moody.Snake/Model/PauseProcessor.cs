using System;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.Common.Extensions;
using Moody.Snake.Model.News;

namespace Moody.Snake.Model
{
    internal class PauseProcessor : IPauseProcessor
    {
        private bool _feedIsActive;
        private readonly INewsFeed _newsFeed;
        private readonly ILogManager _logManager;

        public PauseProcessor(INewsFeed newsFeed, ILogManager logManager)
        {
            _newsFeed = newsFeed;
            _logManager = logManager;
        }

        public bool IsPaused { get; private set; }

        public void ProcessPause()
        {
            IsPaused = !IsPaused;
            if (IsPaused)
                StartPauseFeed().FireAndForgetAsync(_logManager);
            else
            {
                EndPauseFeed();
            }
            
            PauseUpdated?.Invoke(this, EventArgs.Empty);
        }

        private async Task  StartPauseFeed()
        {
            if (_feedIsActive)
                return;
            
            _feedIsActive = true;
            while (_feedIsActive)
            {
                foreach (NewsItem newsItem in _newsFeed.News)
                {
                    NewsUpdated?.Invoke(this, new NewsFeedEventArgs(newsItem));
                    await Task.Delay(8000);
                    if(!_feedIsActive)
                        break;
                }
            }
        }

        private void EndPauseFeed()
        {
            _feedIsActive = false;
        }

        public event EventHandler<NewsFeedEventArgs> NewsUpdated;

        public event EventHandler<EventArgs> PauseUpdated;
    }
}