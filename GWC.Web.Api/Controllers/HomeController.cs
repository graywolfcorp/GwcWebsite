using Gwc.Common.Dtos;
using Gwc.Common.Interfaces.Services;
using Gwc.Common.Utilities.Constants;
using Gwc.Common.Utilities.Helpers;
using GWC.Web.Dtos;
using GWC.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GWC.Web.Api.Controllers
{
    /// <summary>  
    /// Company  
    /// </summary>  
    [Route("api/home")]
    public class HomeController : Controller
    {
        private IBillingCalendarService _calendarService;
        private IConfiguration _configuration { get; }
        private ISendGridService _sendGridService { get; }
        private WeblogConfigurationDto _webConfiguration = new WeblogConfigurationDto();
        /// <summary>  
        /// Constructor for Home Controller  
        /// </summary>  
        public HomeController(
            IBillingCalendarService calendarService, 
            IConfiguration configuration,
            ISendGridService sendGridService
            )
        {
            _calendarService = calendarService;
            _configuration = configuration;
            _sendGridService = sendGridService;
            configuration.GetSection("AppSettings").Bind(_webConfiguration);
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <remarks>
        /// The recaptcha clientResponse is required for the message to be sent
        /// 
        /// </remarks>
        /// <param name="contact"></param>
        /// <response code="200">Returns when message sent</response>
        /// <response code="500">Returns on error</response>
        [HttpPost]
        [Route("contact")]
        public IActionResult SendContactMessage([FromBody] ContactDto contact)
        {
            var secret = _webConfiguration.RecaptchaSecret;          
            contact.Subject = "Contact Form Submission";

            var captchaResponse = CaptchaHelper.GetCaptchaResponse(
               secret,
               contact?.ClientResponse,
               HttpContext.Connection.RemoteIpAddress.ToString());

            if (!captchaResponse.Success)
            {
                return BadRequest();
            }

            var email = new SendGridTransactionalMessageDto
            {
                ApiKey = _webConfiguration.SendGridApiKey,
                Recipients = SendGridConstants.SendGrid_DevelopmentRecipientEmail,
                FromAddress = SendGridConstants.SendGrid_DevelopmentRecipientEmail,
                Subject = "Gray Wolf Contact Form",
                HtmlMessage = 
                    $@"Name: {contact.Name} <br/>
                    Phone: {contact.PhoneNumber} <br/>
                    Email: {contact.Email} <br/>
                    Type: {contact.HelpYou} <br/>
                    Message: {contact.Comments}"
            };

            var response = _sendGridService.SendSingleEmail(email);

            return Ok();
        }
    }
}