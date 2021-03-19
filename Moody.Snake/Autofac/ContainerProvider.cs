using Autofac;
using Moody.Common.Contracts;
using Moody.Snake.Model;
using Moody.Snake.Model.News;
using Moody.Snake.ViewModels;
using Moody.Snake.ViewModels.Game;
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
            containerBuilder.RegisterType<StartViewViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameHeaderViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameView>().AsSelf().ForViewModel(typeof(MainWindowViewModel));
            containerBuilder.RegisterType<RowViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<FieldViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<MainWindow>().AsSelf().ForViewModel(typeof(GameViewViewModel));
            containerBuilder.RegisterType<SnakeLogic>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<MoveLogic>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<NewsFeed>().AsSelf().As<INewsFeed>().As<IInitializable>().SingleInstance();
            containerBuilder.RegisterType<PauseViewModel>().AsSelf();
            
            Registrator.RegisterTypes(containerBuilder);
            Common.Autofac.Registrator.RegisterTypes(containerBuilder);
            
            return containerBuilder.Build();
        }
        
    }
}