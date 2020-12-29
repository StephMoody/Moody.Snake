using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Moody.Snake.ViewModels;

namespace Moody.Snake.Model
{
    internal class SnakeLogic
    {
        private readonly GameWindowViewModel _gameWindowViewModel;
        private FieldViewModel _activeField;

        public SnakeLogic(GameWindowViewModel gameWindowViewModel, FieldViewModel activeField)
        {
            _gameWindowViewModel = gameWindowViewModel;
            _activeField = activeField;
        }

        public Direction CurrentDirection { get; set; }


        public async Task Start()
        {
            _activeField = _gameWindowViewModel.RowViewModels[0].FieldViewModels[0];
            _activeField.Content = FieldContent.Snake;

            await Task.Delay(1000);
            
            while (Move())
            {
                await Task.Delay(1000);
            }
        }

        private bool Move()
        {
            _activeField.Content = FieldContent.Empty;
            
            var newX = _activeField.PositionX + 1;
            if (_gameWindowViewModel.RowViewModels[0].FieldViewModels.Count < newX)
                return false;
            
            _activeField = _gameWindowViewModel.RowViewModels[0].FieldViewModels.First(x=>x.PositionX == newX);
            _activeField.Content = FieldContent.Snake;

            return true;
        }
        
    }
}