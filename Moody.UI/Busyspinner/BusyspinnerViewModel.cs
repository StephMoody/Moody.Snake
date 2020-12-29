using System;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.UI.Busyspinner
{
    public class BusyspinnerViewModel : ViewModelBase
    {
        private bool _stopped;
        public BusyspinnerViewModel(ILogManager logManager) : base(logManager)
        {
            
        }

        public string Text { get; private set; }

        public async Task Start()
        {
            _stopped = false;
            while (!_stopped)
            {
                await Task.Delay(1000);
                Text = DateTime.Now.Millisecond.ToString();
                OnPropertyChanged(nameof(Text));
            }
            
        }

        public void Stop()
        {
            _stopped = true;
        }

    }
}