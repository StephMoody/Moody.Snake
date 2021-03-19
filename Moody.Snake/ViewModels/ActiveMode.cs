using System;
using Moody.Snake.Model;

namespace Moody.Snake.ViewModels
{
    internal class ActiveMode : IActiveMode
    {
        private Mode _mode = Mode.Menu;
        private readonly IPauseProcessor _pauseProcessor;

        public ActiveMode(IPauseProcessor pauseProcessor)
        {
            _pauseProcessor = pauseProcessor;
        }


        public Mode Value => _mode;

        public void SetValue(Mode mode)
        {
            if (mode == Mode.Pause)
            {
                _pauseProcessor.ProcessPause();
            }

            _mode = mode;
            ModeChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> ModeChanged;

    }
}