namespace Moody.Snake.Model.News
{
    public class NewsItem
    {
        public NewsItem(string header, string message)
        {
            Header = header;
            Message = message;
        }

        public string Header { get; }

        public string Message { get; }
    }
}