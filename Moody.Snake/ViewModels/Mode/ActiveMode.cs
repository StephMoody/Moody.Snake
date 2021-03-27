using System;
using Moody.Common.Base;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels.Mode
{
    internal class ActiveMode : IActiveMode
    {
        private ContentModes _contentModes = ContentModes.Menu;
        private readonly IPauseProcessor _pauseProcessor;
        private readonly SnakeLogic _snakeLogic;

        public ActiveMode(IPauseProcessor pauseProcessor, SnakeLogic snakeLogic)
        {
            _pauseProcessor = pauseProcessor;
            _snakeLogic = snakeLogic;
            _snakeLogic.GameOver += SnakeLogicOnGameOver;
        }
        
        public ContentModes Value => _contentModes;

        public void SetValue(ContentModes contentModes)
        {
            
            if (contentModes == ContentModes.Pause)
            {
                _pauseProcessor.ProcessPause();
            }

            ContentModes oldValue = _contentModes;
            _contentModes = contentModes;
            ModeChanged?.Invoke(this, new ValueChangedEventArgs<ContentModes>( _contentModes, oldValue));
        }

        public event EventHandler<ValueChangedEventArgs<ContentModes>> ModeChanged;
        
        private void SnakeLogicOnGameOver(object sender, EventArgs e)
        {
            try
            {
                SetValue(ContentModes.GameOver);
            }
            catch (Exception exception)
            {
                //
            }
        }

    }
}