using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ConvertVolumeFlowRateController : Controller
    {
        // GET api/convertvolumeflowrate/5/
        [HttpGet("{value}/{toCfm}")]
        [AllowAnonymous]
        public IActionResult Get(string value, string toCfm)
        {
            decimal dValue = Convert.ToDecimal(value);
            bool bToCfm = false;
            if ( UMCLib.TryConvertToBoolean(toCfm, out bToCfm))
            {
                decimal dResult = AirFlowTechTools.CalcVolumeFlowRate(dValue, bToCfm);    
                return Ok(Convert.ToString(dResult));
            }

            return BadRequest("Error in ConvertVolumeFlowRateController, check parameters.");
        }
    }
}
