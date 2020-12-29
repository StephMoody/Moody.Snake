using System;
using Moody.UI.WindowHandling;
using Moody.UI.Windows;

namespace Moody.UI.Busyspinner
{
    public class BusyspinnerProvider
    {
        
        
        public IDisposable ShowBusyspinner()
        {
            return new Busyspinner(()=> new WindowShower<BusyspinnerWindow, BusyspinnerViewModel>(new BusyspinnerWindow(), new BusyspinnerViewModel(null))).Show();
        }
    }
}