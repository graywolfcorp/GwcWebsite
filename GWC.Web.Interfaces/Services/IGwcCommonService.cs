using GWC.Web.Interfaces.Services;
using GWC.Web.Interfaces.Data;
using Gwc.Common.Interfaces.Services;
using GWC.Web.Dtos;

namespace GWC.Web.Interfaces.Services
{
    public interface IGwcCommonService
    {
        WeblogConfigurationDto Configuration { get; set; }
        IGwcUnitOfWork UnitOfWork { get; set; }
        ILogManagerService LogManager { get; set; }
    }
}
