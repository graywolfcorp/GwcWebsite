using GWC.Web.Interfaces.Services;
using GWC.Web.Interfaces.Data;
using Gwc.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using GWC.Web.Dtos;

namespace GWC.Web.Services
{
    public class GwcCommonService : IGwcCommonService
    {
        public IGwcUnitOfWork UnitOfWork { get; set; }
        public ILogManagerService LogManager { get; set; }
        public WeblogConfigurationDto Configuration { get; set; }

        public GwcCommonService(IGwcUnitOfWork unitOfWork, ILogManagerService logManager, IConfiguration configuration )
        {
            Configuration = new WeblogConfigurationDto();
            UnitOfWork = unitOfWork;
            LogManager = logManager;
            configuration.GetSection("AppSettings").Bind(Configuration);

        }

    }
}
