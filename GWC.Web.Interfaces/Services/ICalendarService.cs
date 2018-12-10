using GWC.Web.Dtos;
using GWC.Web.Dtos.Requests;
using System.Collections.Generic;

namespace GWC.Web.Interfaces.Services
{
    public interface ICalendarService
    {
        IEnumerable<CalendarDto> SearchCalendar(CalendarSearchRequest request);
        CalendarDto GetCalendarById(int id);
        CalendarDto AddCalendar(CalendarDto calendarDto);
        CalendarDto UpdateCalendar(CalendarDto calendarDto);
        void DeleteCalendar(int id);
    }
}