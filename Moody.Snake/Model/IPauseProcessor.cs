using System;

namespace Moody.Snake.Model
{
    internal interface IPauseProcessor
    {
        bool IsPaused { get; }
        void ProcessPause();
        event EventHandler<NewsFeedEventArgs> NewsUpdated;
        event EventHandler<EventArgs> PauseUpdated;
    }
}