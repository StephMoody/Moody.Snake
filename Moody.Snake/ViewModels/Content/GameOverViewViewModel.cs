using System.Windows.Input;
using Moody.Common.Contracts;

namespace Moody.Snake.ViewModels.Content
{
    internal class GameOverViewViewModel : ContentViewModelBase
    {
        public GameOverViewViewModel(ILogManager logManager) : base(logManager)
        {
        }

        public override void HandleKeyDown(Key key)
        {
            
        }
    }
}