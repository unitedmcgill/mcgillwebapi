using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McGillWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using McGillWebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]  use OAuth2 flows 
    // JwtBearer -- Microsoft.AspNetCore.AuthenticationJwtBearer to package.json
    
    public class NotifyPersonnelController : Controller
    {
        //private MailService _mailService = new MailService();
        
        private IMailService _mailService;
        private IConfigurationRoot _config;

        // Inject our mailservice (mapped in Startup.cs)
        public NotifyPersonnelController (IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        // POST api/notifypersonnel
        // Is nice to use a ViewModel instead of the Models
        // Then put [Required], etc. on the ViewModel instead of the Model
        // This will allow you to hide the information like Id, etc.
        // basically stuff not used in the view
        // [Produces(typeof(Contact)) ]
        // [Produces("application/json", Type = typeof(Contact)) ]
        // You can limit received types with [Consumes]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]Contact theContact)
        {
            // Do something
            if (ModelState.IsValid)
            {
                // Email Kathy for now
                await _mailService.SendMail(theContact.Email, 
                theContact.Name, "personnel@unitedmcgill.com", "Application id#"+theContact.Company+" was saved", 
                theContact.TheMessage, null);

                return Created($"api/notifypersonnel/{theContact.Name}", theContact);

                // If you need to save to the Database see link
                // Use AutoMappers, they are cool!
                // https://app.pluralsight.com/player?course=aspdotnetcore-efcore-bootstrap-angular-web-app&author=shawn-wildermuth&name=aspdotnetcore-efcore-bootstrap-angular-web-app-m7&clip=4&mode=live
                //return Ok(true);
            }

            return BadRequest(ModelState);
        }
    }
}
