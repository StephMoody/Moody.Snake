using System.Net.Mime;
using System.Threading.Tasks;
using Moody.Common.Contracts;
using Moody.MVVM.Base.ViewModel;

namespace Moody.Snake.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private bool _enterpressed;
        private readonly GameViewViewModel _gameViewViewModel;

        public MainWindowViewModel(ILogManager logManager, GameViewViewModel gameViewViewModel) : base(logManager)
        {
            _gameViewViewModel = gameViewViewModel;
        }

        public override Task Initialize()
        {
            _gameViewViewModel.Initialize();
            return base.Initialize();
        }

        public GameViewViewModel GameViewViewModel => _gameViewViewModel;
        
        public bool EnterPressed
        {
            get { return _enterpressed; }
            set
            {
                _enterpressed = value;
                OnPropertyChanged();
            }
        }
    }
}