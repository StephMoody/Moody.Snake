using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;
using Moody.Snake.Model;
using Moody.Snake.ViewModels.Mode;

namespace Moody.Snake.ViewModels.Content
{
    internal class PauseViewModel : ContentViewModelBase
    {
        private string _newsHeader;
        private string _newsMessage;
        [NotNull] private readonly IPauseProcessor _pauseProcessor;
        [NotNull] private readonly IActiveMode _activeMode;

        public PauseViewModel(ILogManager logManager, IPauseProcessor pauseProcessor, IActiveMode activeMode) : base(logManager)
        {
            _pauseProcessor = pauseProcessor;
            _activeMode = activeMode;
        }

        public override Task Initialize()
        {
            _pauseProcessor.NewsUpdated += PauseProcessorOnNewsUpdated;
            return base.Initialize();
        }

        public override void HandleKeyDown(Key key)
        {
            if(key == Key.Escape || key == Key.P)
                _activeMode.SetValue(ContentModes.Game);
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