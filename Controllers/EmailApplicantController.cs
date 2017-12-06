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
    
    public class EmailApplicantController : Controller
    {
        //private MailService _mailService = new MailService();
        
        private IMailService _mailService;
        private IConfigurationRoot _config;

        // Inject our mailservice (mapped in Startup.cs)
        public EmailApplicantController (IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        // POST api/emailapplicant
        [HttpPost]
        [Authorize(Policy = "DisneyUser")]
        public async Task<IActionResult> Post([FromBody]Contact theContact)
        {
            // Do something
            if (ModelState.IsValid)
            {
                await _mailService.SendMail(theContact.Email, 
                theContact.Name, "personnel@unitedmcgill.com", "A message from Personnel Services at McGill", 
                theContact.TheMessage, "kcauley@unitedmcgill.com");

                return Created($"api/emailapplicant/{theContact.Name}", theContact);

                // If you need to save to the Database see link
                // Use AutoMappers, they are cool!
                // https://app.pluralsight.com/player?course=aspdotnetcore-efcore-bootstrap-angular-web-app&author=shawn-wildermuth&name=aspdotnetcore-efcore-bootstrap-angular-web-app-m7&clip=4&mode=live
                //return Ok(true);
            }

            return BadRequest(ModelState);
        }

        // GET api/contactus/5
        // [HttpGet("{id}")]  // FYI, {id:int} inline constraint will force the parameter to only match if an int parameter is specified
        // [AllowAnonymous]
        // public Contact Get(int id)
        // {
        //     return null; //Get().Where( r => r.Id == id).FirstOrDefault();;
        // }

        // [HttpGet]
        // [AllowAnonymous]
        // public IActionResult Get(){
        //     // If soemthing bad happended, we can return an error
        //     // return BadRequest("bad things happened")  // 400 status w/ message is returned

        //     return Ok(Json(new Contact() { Id = 1, Name = "Joe", TheMessage = "Hello there!"} ));
        // }
    }
}
