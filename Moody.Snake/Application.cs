﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moody.Common.Contracts;
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

            IEnumerable<IInitializable> initializable = lifeTimeScope.Resolve<IEnumerable<IInitializable>>();
            foreach (IInitializable initializableItem in initializable)
            {
               await initializableItem.Initialize();
            }
            
            SnakeLogic snakeLogic = lifeTimeScope.Resolve<SnakeLogic>();
            snakeLogic.Initialize(30);
            IWindowShower<MainWindow, MainWindowViewModel> windowShower = lifeTimeScope.Resolve<IWindowShower<MainWindow, MainWindowViewModel>>();
            await windowShower.ViewModel.Initialize();
            windowShower.Show();
            
            await snakeLogic.Start();
        }
    }
}