using GWC.Web.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWC.Web.Testing.Helpers
{
    public class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath, bool developmentMode)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile(developmentMode ? "appsettings.Development.json" : "appsettings.json", optional: true)
                //.AddUserSecrets("e3dfcccf-0cb3-423a-b302-e3e92e95c128")
                .AddEnvironmentVariables()
                .Build();
        }

        public static WeblogConfigurationDto GetApplicationConfiguration(string outputPath, bool developmentMode)
        {
            var configuration = new WeblogConfigurationDto();

            var iConfig = GetIConfigurationRoot(outputPath, developmentMode);

            iConfig
                .GetSection("AppSettings")
                .Bind(configuration);

            return configuration;
        }
    }
}
