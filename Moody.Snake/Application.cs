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
            SnakeLogic snakeLogic = lifeTimeScope.Resolve<SnakeLogic>();
            snakeLogic.Initialize(30);
            IWindowShower<GameWindow, GameWindowViewModel> windowShower = lifeTimeScope.Resolve<IWindowShower<GameWindow, GameWindowViewModel>>();
            await windowShower.ViewModel.Initialize();
            windowShower.Show();
            
            await snakeLogic.Start();
        }
    }
}