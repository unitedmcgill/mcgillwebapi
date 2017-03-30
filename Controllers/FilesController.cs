using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McGillWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using McGillWebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Http;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using McGill.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]  use OAuth2 flows 
    // JwtBearer -- Microsoft.AspNetCore.AuthenticationJwtBearer to package.json
    
    public class FilesController : Controller
    {
        //private MailService _mailService = new MailService();
        
        private IConfigurationRoot _config;
        private readonly IHostingEnvironment _hostingEnvironment;

        // Inject our mailservice (mapped in Startup.cs)
        public FilesController(IHostingEnvironment hostingEnvironment, IConfigurationRoot config)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
        }

        // POST api/files
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post()
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                var foo = new DoMathThings();
                int result = foo.AddTwoNumbers(1,2);
                long size = 0;
                var savePath = "";
                Microsoft.AspNetCore.Http.IFormFile lastFile = null;

                foreach(var file in Request.Form.Files)
                {
                    var contentType = file.ContentType;
                    var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    size += file.Length;
                    savePath = Path.Combine(_hostingEnvironment.WebRootPath, _config["FileUploadSettings:Uploads"], filename);  

                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.OpenReadStream().CopyToAsync(fileStream);
                    }
                    lastFile = file;
                }
                
                return Created(savePath, lastFile);
            }

            return BadRequest();
        }
        
        // GET api/files/code = 10-digit code 0-9, A-F
        [HttpGetAttribute("{code}")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            string sErrorMsg = String.Empty;
            List<ShareFileData> outputFiles = new List<ShareFileData>();
            
            return Ok();
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
