using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GWC.Web.Interfaces.Services;
using System;
using Gwc.Common.Utilities.Extensions;

namespace GWC.Web.Api.Controllers
{
#pragma warning disable 1591
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/[controller]")]
    public class BillingCalendarController : Controller
    {
        private IBillingCalendarService _calendarService;
        public BillingCalendarController(IBillingCalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var userDtos = _calendarService.GetAll();
            return Ok(userDtos);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userDto = _calendarService.GetById(id);
            return Ok(userDto);
        }
        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult GetTest()
        {
            return Ok(DateTime.Now.ToLongDateString().Add("test")  );
        }
    }

#pragma warning restore 1591
}