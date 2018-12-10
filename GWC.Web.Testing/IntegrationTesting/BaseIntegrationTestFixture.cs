using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Gwc.Common.Interfaces.Services;
using GWC.Web.DataAccess;
using GWC.Web.Dtos;
using GWC.Web.Interfaces.Services;
using GWC.Web.Services.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace GWC.Web.Testing.IntegrationTesting
{
    public class BaseIntegrationTestFixture
    {
        public static IGwcCommonService gwcCommonService;
        public static IBillingCalendarService billingCalendarService;
        public static ICalendarService calendarService;
        public static IContentfulService contentfulService;
        public static ISourceService sourceService;
        public static ILoggingService loggingService;
        public static ILogManagerService logManagerService;

        [SetUpFixture]
        public class IntegrationTests
        {
            [OneTimeSetUp]
            public void Init()
            {
                var webConfiguration = new WeblogConfigurationDto();
                
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(TestContext.CurrentContext.TestDirectory)                
                    .AddUserSecrets("8cef12cd-ce27-4c76-adca-413d9edc4ccd")
                    .AddEnvironmentVariables()
                    .Build();

                configuration.GetSection("AppSettings").Bind(webConfiguration);

                var services = new ServiceCollection();

                services.AddAutoMapper(x => x.AddProfile<GwcModelDtoProfile>());

                services.AddDbContext<GwcDbContext>(cfg =>
                {
                    cfg.UseSqlServer(webConfiguration.ConnectionString);
                });

                var containerBuilder = new ContainerBuilder();
                containerBuilder.Register(x => configuration).As<IConfiguration>().InstancePerLifetimeScope();
                AutofacConfig.Register(containerBuilder);
                containerBuilder.Populate(services);
                var applicationContainer = containerBuilder.Build();
                var serviceProvider = new AutofacServiceProvider(applicationContainer);

                logManagerService = serviceProvider.GetService<ILogManagerService>();
                loggingService = serviceProvider.GetService<ILoggingService>();
                loggingService.AddDatabaseTarget(webConfiguration.ConnectionString, webConfiguration.LogLevel, logManagerService.DefaultDbLogger);
                //loggingService.AddFileTarget(configuration.logFolder.Add("nLogTesting.txt"), configuration.logLevel, logManagerService.DefaultFileLogger);

                gwcCommonService = serviceProvider.GetService<IGwcCommonService>();
                billingCalendarService = serviceProvider.GetService<IBillingCalendarService>();
                contentfulService = serviceProvider.GetService<IContentfulService>();
                calendarService = serviceProvider.GetService<ICalendarService>();
                sourceService = serviceProvider.GetService<ISourceService>();

                var logger = logManagerService.GetDefaultDbLoggingService();
                //logger.Info("created DbLogging Test 6");
            }
        }
    }
}