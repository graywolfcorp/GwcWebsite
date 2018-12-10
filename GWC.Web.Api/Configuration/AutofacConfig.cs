using Autofac;
using Gwc.Common.Interfaces.Services;
using Gwc.Common.Services;
using GWC.DataAccess;
using GWC.Web.DataAccess;
using GWC.Web.Interfaces.Data;
using System.Linq;
using System.Reflection;


namespace GWC.Web.Configuration
{
#pragma warning disable 1591
    public static class AutofacConfig
    {
        private static bool _developmentMode;

        public static void Register(ContainerBuilder builder, bool developmentMode)
        {
            _developmentMode = developmentMode;
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
        }

        public class EFModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                

                builder.RegisterType<GwcDbContext>().As<IGwcDbContext>().InstancePerLifetimeScope();
                builder.RegisterType(typeof(GwcUnitOfWork)).As(typeof(IGwcUnitOfWork)).InstancePerLifetimeScope();
            }
        }

        public class ServiceModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterAssemblyTypes(Assembly.Load("GWC.Common.Services"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

                builder.RegisterAssemblyTypes(Assembly.Load("GWC.Web.Services"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

                builder.RegisterType<LoggingService>().As<ILoggingService>().InstancePerLifetimeScope();
                builder.RegisterType<LogManagerService>().As<ILogManagerService>().InstancePerLifetimeScope();
            }
        }    
    }
#pragma warning restore 1591
}

