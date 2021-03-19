using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Moody.Common.Contracts;

namespace Moody.Snake.Model.News
{
    public class NewsFeed : INewsFeed,IInitializable
    {
        private readonly List<NewsItem> _news = new List<NewsItem>();

        public int Order => 1;
        public Task Initialize()
        {
            string url = "https://www.tagesschau.de/xml/rss2";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                News.Add(new NewsItem(item.Title.Text, item.Summary.Text));
            }
            
            return Task.CompletedTask;
        }
        
        public List<NewsItem> News => _news;
    }
}