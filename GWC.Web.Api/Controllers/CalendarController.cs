using Gwc.Common.Utilities.Extensions;
using GWC.Web.Dtos.Requests;
using GWC.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GWC.Web.Api.Controllers
{
    /// <summary>  
    /// Calendars  
    /// </summary>  
    [ApiController]
    [Route("api/calendars")]
    public class CalendarController : Controller
    {
        private ICalendarService _calendarService;
        /// <summary>  
        /// Constructor for Home Controller  
        /// </summary>  
        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        /// <summary>
        /// Gets a list of calendar events
        /// </summary>
        /// <remarks>
        /// 
        /// 
        /// </remarks>
        /// <param name="calendarSearchRequest"></param>
        /// <response code="200">Returns when events returned</response>
        /// <response code="500">Returns on error</response>
        [HttpGet]
        public IActionResult SearchCalendars([FromBody] CalendarSearchRequest calendarSearchRequest)
        {
            var userDtos = _calendarService.SearchCalendar(calendarSearchRequest);
            return Ok(userDtos);
        }
        /// <summary>
        /// Gets a single calendar event
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns when event found</response>
        /// <response code="400">Returns when event not found</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userDto = _calendarService.GetCalendarById(id);
            return Ok(userDto);
        }

        /// <summary>
        /// Api hearbeat test
        /// </summary>        
        /// <response code="200">Returns when endpoint is working</response>
        /// <response code="500">Returns when endpoint is not working</response>
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult GetTest()
        {
            return Ok(DateTime.Now.ToLongDateString().Add("Calendar Controller is working.")  );
        }
    }
}