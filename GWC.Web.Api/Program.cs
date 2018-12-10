using GWC.Web.DataAccess;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GWC.Web.Api
{
#pragma warning disable 1591
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
                //.UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()                
                .Build();

        //must change ASPNETCORE_ENVIRONMENT in package manage console to update production with update-database
        //$Env:ASPNETCORE_ENVIRONMENT = "DevelopmentX"
        public class GwcDbContexttFactory : IDesignTimeDbContextFactory<GwcDbContext>
        {
            public GwcDbContext CreateDbContext(string[] args)
            {                
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var jsonFile = environmentName == "Development" ? "appsettings.Development.json" : "appsettings.json";

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(jsonFile, optional:false, reloadOnChange: true)
                    .Build();
                var builder = new DbContextOptionsBuilder<GwcDbContext>();
                var connectionString = configuration.GetConnectionString("graywolf");
                builder.UseSqlServer(connectionString);
                return new GwcDbContext(builder.Options);
            }
        }
    }
#pragma warning restore 1591
}
