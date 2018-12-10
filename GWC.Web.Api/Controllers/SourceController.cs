using Gwc.Common.Utilities.Extensions;
using GWC.Web.Dtos;
using GWC.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace GWC.Web.Api.Controllers
{
    /// <summary>  
    /// Sources  
    /// </summary>  
    [ApiController]
    [Route("api/sources")]
    public class SourceController : Controller
    {
        private ISourceService _sourceService;
        private IConfiguration _configuration;
        private IHostingEnvironment _environment;
        private WeblogConfigurationDto _webConfiguration = new WeblogConfigurationDto();
        /// <summary>  
        /// Constructor for Home Controller  
        /// </summary>  
        public SourceController(ISourceService sourceService, IConfiguration configuration, IHostingEnvironment environment)
        {
            _sourceService = sourceService;
            _configuration = configuration;
            _environment = environment;
            configuration.GetSection("AppSettings").Bind(_webConfiguration);
        }

        /// <summary>
        /// Gets a list of source events
        /// </summary>
        /// <remarks>
        /// 
        /// 
        /// </remarks>
        /// <response code="200">Returns when events returned</response>
        /// <response code="500">Returns on error</response>
        [HttpGet]
        public IActionResult SearchSources()
        {
            var userDtos = _sourceService.SearchSource();
            return Ok(userDtos);
        }
        /// <summary>
        /// Gets a single source event
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns when event found</response>
        /// <response code="400">Returns when event not found</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userDto = _sourceService.GetSourceById(id);
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
            return Ok($"{DateTime.Now.ToLongDateString()} - Source Controller is working.");
        }

        /// <summary>
        /// Check AppSetting Log Configuration
        /// </summary>        
        /// <response code="200">Returns when endpoint is working</response>
        /// <response code="500">Returns when endpoint is not working</response>
        [HttpGet("config")]
        [AllowAnonymous]
        public IActionResult GetConfig()
        {            
            return Ok(_webConfiguration.LogLevel);
        }

    }
}