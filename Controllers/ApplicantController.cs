using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;
using McGillWebAPI.Models;
using McGillWebAPI.Model;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace McGillWebAPI.Controllers
{   
    [Route("api/[controller]")]
    public class ApplicantController : Controller
    {   
        // GET list of applicants
        [HttpGet]
        [Authorize(Policy = "DisneyUser")]  // I'm just using the example and never changed DisneyUser :)
        // [AllowAnonymous]
        public IEnumerable<EmploymentApp> Get()
        {
            // AirSilenceContext context = new AirSilenceContext();
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                var apps = umc.EmploymentApp
                        .OrderBy(t => t.LastName)
                        .ThenBy(t => t.FirstName)
                        .ToList();
                
                return apps;
            }
        }

        // GET api/applicant/15
        [HttpGet("{id}")]
        [Authorize(Policy = "DisneyUser")]
        public EmploymentApp Get(int id)
        {
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                var app =  umc.EmploymentApp
                                            .First(t => t.EmploymentAppId == id);
                return app;
            } 
        }

        // POST api/applicant
        [HttpPost]
        [Authorize(Policy = "DisneyUser")]
        public IActionResult Post([FromBody] EmploymentApp app )
        {
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                umc.EmploymentApp.Add(app);
                umc.SaveChanges();
            }

            return Ok(app);
        }

        // PUT api/applicant/766
        [HttpPut("{id}")]
        [Authorize(Policy = "DisneyUser")]
        public IActionResult Put(int id, [FromBody] EmploymentApp app)
        {
            // Convert SSNs
            // string[] lines = System.IO.File.ReadAllLines(@"C:\temp\ssn.txt");
            // foreach( string line in lines)
            // {
            //     string[] items = line.Split(' ');
            //     int myId;
            //     UMCLib.TryConvertToInt32(items[0], out myId);
            //     string encSSN = OnlineAppController.EncryptSSN(items[1]);
            //     //string decryptSSN = OnlineAppController.DecryptSSN(encSSN);
            //     EmploymentApp mod = Get(myId);
            //     using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            //     {
            //         mod.EmploymentAppId = myId;
            //         mod.SectionC.Ssn = encSSN;
            //         umc.EmploymentApp.Update(mod);
            //         umc.SaveChanges();
            //     }
            // }

            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                app.EmploymentAppId = id;
                umc.EmploymentApp.Update(app);
                umc.SaveChanges();
            }

            return Ok();
        }

        // DELETE api/applicant/766
        [HttpDelete("{id}")]
        [Authorize(Policy = "DisneyUser")]
        public IActionResult Delete(int id)
        {
            using ( UnitedMcGillContext umc = new UnitedMcGillContext() )
            {
                // Ensure id exists
                if ( umc.EmploymentApp.Where(t => t.EmploymentAppId == id).Count() > 0 )
                {
                    umc.EmploymentApp.Remove(umc.EmploymentApp.First(t => t.EmploymentAppId == id));
                    umc.SaveChanges();
                }
            }

            return Ok();
        }  
    }
}
