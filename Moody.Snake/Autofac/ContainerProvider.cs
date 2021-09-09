using Autofac;
using Moody.Common.Contracts;
using Moody.Snake.Model;
using Moody.Snake.Model.Game;
using Moody.Snake.Model.News;
using Moody.Snake.ViewModels;
using Moody.Snake.ViewModels.Content;
using Moody.Snake.ViewModels.Game;
using Moody.Snake.ViewModels.Mode;
using Moody.Snake.Windows;
using Moody.UI.Autofac;

namespace Moody.Snake.Autofac
{
    internal class ContainerProvider
    {
        public ILifetimeScope Build()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            
            RegisterViewModel(containerBuilder);
            RegisterModel(containerBuilder);
            containerBuilder.RegisterType<MainWindow>().AsSelf().ForViewModel(typeof(MainWindowViewModel));
            
            Registrator.RegisterTypes(containerBuilder);
            Common.Autofac.Registrator.RegisterTypes(containerBuilder);
            
            return containerBuilder.Build();
        }

        private void RegisterModel(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MoveProcessor>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameField>().As<IGameField>().SingleInstance();
            containerBuilder.RegisterType<Model.Game.Snake>().As<ISnake>().SingleInstance();
            containerBuilder.RegisterType<MoveCalculator>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<NewsFeed>().AsSelf().As<INewsFeed>().As<IInitializable>().SingleInstance();
            containerBuilder.RegisterType<PauseProcessor>().As<IPauseProcessor>().SingleInstance();
            containerBuilder.RegisterType<ActiveMode>().As<IActiveMode>().SingleInstance();
            containerBuilder.RegisterType<FieldState>().As<IFieldState>().InstancePerDependency();
        }

        private void RegisterViewModel(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<GameViewViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<StartViewViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameHeaderViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<RowViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<FieldViewModel>().AsSelf().InstancePerDependency();
            containerBuilder.RegisterType<PauseViewModel>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<GameOverViewViewModel>().AsSelf().SingleInstance();
        }
    }
}