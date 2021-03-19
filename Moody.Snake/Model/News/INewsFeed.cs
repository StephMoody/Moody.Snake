using System.Collections.Generic;

namespace Moody.Snake.Model.News
{
    public interface INewsFeed
    { 
        List<NewsItem> News { get; }
    }
}