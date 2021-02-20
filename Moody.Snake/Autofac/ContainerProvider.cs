using Autofac;
using Moody.Snake.Model;
using Moody.Snake.ViewModels;
using Moody.Snake.Windows;
using Moody.UI.Autofac;

namespace Moody.Snake.Autofac
{
    internal class ContainerProvider
    {
        public ILifetimeScope Build()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<GameViewViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameHeaderViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameView>().AsSelf().ForViewModel(typeof(MainWindowViewModel));
            containerBuilder.RegisterType<RowViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<FieldViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<MainWindow>().AsSelf().ForViewModel(typeof(GameViewViewModel));
            containerBuilder.RegisterType<SnakeLogic>().AsSelf().SingleInstance();
            
            Registrator.RegisterTypes(containerBuilder);
            Common.Autofac.Registrator.RegisterTypes(containerBuilder);
            
            return containerBuilder.Build();
        }
        
    }
}