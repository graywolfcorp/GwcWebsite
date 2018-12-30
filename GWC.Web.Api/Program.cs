using GWC.Web.DataAccess;
using GWC.Web.Dtos;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GWC.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseAzureAppServices()
                .UseStartup<Startup>()
                .UseApplicationInsights()                
                .Build();

        public class GwcDbContexttFactory : IDesignTimeDbContextFactory<GwcDbContext>
        {
            public GwcDbContext CreateDbContext(string[] args)
            {
                var webConfiguration = new WeblogConfigurationDto();
                var configuration = new ConfigurationBuilder()
                    .AddUserSecrets<Startup>()
                    .AddEnvironmentVariables()
                    .Build();

                configuration.GetSection("AppSettings").Bind(webConfiguration);

                var builder = new DbContextOptionsBuilder<GwcDbContext>();                
                builder.UseSqlServer(webConfiguration.ConnectionString);
                return new GwcDbContext(builder.Options);
            }
        }
    }
}
