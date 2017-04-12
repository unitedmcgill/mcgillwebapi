using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;
using McGillWebAPI.Models;

namespace McGillWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PressureBurst : Controller
    {
        // POST api/pressureburst/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CalcOperatingPressure operatingPressure )
        {
            // Material

            int Gauge;
            if ( !UMCLib.TryConvertToInt32(operatingPressure.Gauge, out Gauge))
            {
                return BadRequest("Error in PressureBurstController, Gauge convert failed.");
            }

            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(operatingPressure.Spiral, out Spiral))
            {
                return BadRequest("Error in PressureBurstController, Spiral onvert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.Diameter, out Diameter))
            {
                return BadRequest("Error in PressureBurstController, Diameter convert failed.");
            }

            decimal DuctTemp;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.DuctTemp, out DuctTemp))
            {
                return BadRequest("Error in PressureBurstController, DuctTemp convert failed.");
            }

            decimal BurstPressure;
            BurstPressure = AirFlowTechTools.CalcBurstPressure( operatingPressure.Material, Gauge, Spiral, Diameter, DuctTemp);
            operatingPressure.OperatingPressure = BurstPressure/10;
            operatingPressure.Pressure = BurstPressure;
            
            return Ok(operatingPressure);
        }
    }
}
