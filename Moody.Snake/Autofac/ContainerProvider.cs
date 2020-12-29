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
            containerBuilder.RegisterType<GameWindowViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<RowViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<FieldViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<GameWindow>().AsSelf().ForViewModel(typeof(GameWindowViewModel));
            containerBuilder.RegisterType<SnakeLogic>().AsSelf().SingleInstance();
            
            Registrator.RegisterTypes(containerBuilder);
            Common.Autofac.Registrator.RegisterTypes(containerBuilder);
            
            return containerBuilder.Build();
        }
        
    }
}