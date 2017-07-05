using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CheckUserController : Controller
    {
        // GET api/checkuser
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            IPAddress clientIP = Request.HttpContext.Connection.RemoteIpAddress;
            string IP = clientIP.ToString();
            if ( IP.Length >= 3)
            {
                if (IP.Substring(0,3)=="::1")
                {
                    return Ok("1");
                }

                if ( IP.Length >= 7 )
                {
                    if ( IP.Substring(0,3)=="192" )
                    {
                        if ( IP.Substring(5,3)=="168")
                        {
                            return Ok("1");
                        }
                    }
                }
            }

            return Ok("0");
        }
    }
}
