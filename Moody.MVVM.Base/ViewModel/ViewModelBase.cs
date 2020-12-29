using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moody.Common.Contracts;

namespace Moody.MVVM.Base.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase(ILogManager logManager)
        {
            LogManager = logManager;
        }

        public virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected ILogManager LogManager { get; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}