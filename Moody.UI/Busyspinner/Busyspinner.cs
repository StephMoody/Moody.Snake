using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Moody.Common.Extensions;
using Moody.UI.WindowHandling;
using Moody.UI.Windows;

namespace Moody.UI.Busyspinner
{
    internal class Busyspinner : IDisposable
    {
        
        
        private readonly Func< WindowShower<BusyspinnerWindow, BusyspinnerViewModel>> _windowShowerCreator;
        private Dispatcher _offThreadDispatcher;
        private WindowShower<BusyspinnerWindow, BusyspinnerViewModel> _windowShower;

        public Busyspinner(Func< WindowShower<BusyspinnerWindow, BusyspinnerViewModel>> windowShower)
        {
            _windowShowerCreator = windowShower;
        }

        public IDisposable Show()
        {
            
            
            var t = new Thread(() => ShowBusySpinnerWindow());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            // _offThreadDispatcher.Invoke(() => _windowShower.ViewModel.Start());
            
            return this;
        }

        private void ShowBusySpinnerWindow()
        {
            _windowShower = _windowShowerCreator();
            _offThreadDispatcher = Dispatcher.CurrentDispatcher;
            _windowShower.ViewModel.Start().FireAndForgetAsync(null);
            _windowShower.Show();
        }
        
        public void Dispose()
        {
            _offThreadDispatcher.Invoke(() =>
           {
               _windowShower.ViewModel.Stop();
               _windowShower.Close();
           });  ;
        }
    }
}