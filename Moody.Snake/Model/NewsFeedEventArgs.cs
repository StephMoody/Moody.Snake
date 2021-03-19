using System;
using Moody.Snake.Model.News;

namespace Moody.Snake.Model
{
    internal class NewsFeedEventArgs : EventArgs
    {
        public NewsFeedEventArgs(NewsItem newsItem)
        {
            NewsItem = newsItem;
        }

        public NewsItem NewsItem { get; }
    }
}