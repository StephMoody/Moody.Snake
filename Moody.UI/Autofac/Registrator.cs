using Autofac;
using Moody.UI.Contracts;
using Moody.UI.WindowHandling;

namespace Moody.UI.Autofac
{
    public static class Registrator
    {
        public static void RegisterTypes(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(WindowShower<,>)).As(typeof(IWindowShower<,>))
                .InstancePerLifetimeScope()
                .WithViewModelParameter();
        }
    }
}