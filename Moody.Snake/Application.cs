using System.Threading.Tasks;
using Autofac;
using Moody.Snake.Autofac;
using Moody.Snake.Model;
using Moody.Snake.ViewModels;
using Moody.Snake.Windows;
using Moody.UI.Contracts;

namespace Moody.Snake
{
    internal static class Application
    {
        public static async Task Run()
        {
            ILifetimeScope lifeTimeScope = new ContainerProvider().Build();
            IWindowShower<GameWindow, GameWindowViewModel> windowShower = lifeTimeScope.Resolve<IWindowShower<GameWindow, GameWindowViewModel>>();
            windowShower.ViewModel.Initialize(6);
            windowShower.Show();

            await Task.Delay(5000);
            
            SnakeLogic snakeLogic = lifeTimeScope.Resolve<SnakeLogic>();
            await snakeLogic.Start();
        }
    }
}