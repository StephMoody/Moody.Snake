using System;
using Moody.Common.Base;
using Moody.Snake.Model;
using Moody.Snake.Model.Game;

namespace Moody.Snake.ViewModels.Mode
{
    internal class ActiveMode : IActiveMode
    {
        private ContentModes _contentModes = ContentModes.Menu;
        private readonly IPauseProcessor _pauseProcessor;
        private readonly MoveProcessor _moveProcessor;

        public ActiveMode(IPauseProcessor pauseProcessor, MoveProcessor moveProcessor)
        {
            _pauseProcessor = pauseProcessor;
            _moveProcessor = moveProcessor;
            _moveProcessor.GameOver += MoveProcessorOnGameOver;
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
        
        private void MoveProcessorOnGameOver(object sender, EventArgs e)
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