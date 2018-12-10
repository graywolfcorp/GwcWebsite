using GWC.Web.Dtos;
using System.Collections.Generic;


namespace GWC.Web.Interfaces.Services
{
    public interface IBillingCalendarService
    {
        IEnumerable<BillingCalendarDto> GetAll();
        BillingCalendarDto GetById(int id);
    }
}
