using Autofac;
using Moody.Common.Contracts;
using LogManager = Moody.Common.Logging.LogManager;

namespace Moody.Common.Autofac
{
    public static class Registrator
    {
        public static void RegisterTypes(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<LogManager>().As<ILogManager>();
        }
    }
}