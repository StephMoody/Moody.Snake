using System.Windows.Input;
using Moody.Common.Contracts;
using Moody.Snake.ViewModels.Mode;

namespace Moody.Snake.ViewModels.Content
{
    internal class StartViewViewModel : ContentViewModelBase
    {
        private readonly IActiveMode _activeMode;
        
        public StartViewViewModel(ILogManager logManager, IActiveMode activeMode) : base(logManager)
        {
            _activeMode = activeMode;
        }

        public override void HandleKeyDown(Key key)
        {
            _activeMode.SetValue(ContentModes.Game);
        }
    }
}