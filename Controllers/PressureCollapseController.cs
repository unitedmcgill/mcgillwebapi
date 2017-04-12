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
    public class PressureCollapse : Controller
    {
        // POST api/pressurecollapse/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CalcOperatingPressure operatingPressure )
        {
            // Material

            int Gauge;
            if ( !UMCLib.TryConvertToInt32(operatingPressure.Gauge, out Gauge))
            {
                return BadRequest("Error in PressureCollapseController, Gauge convert failed.");
            }

            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(operatingPressure.Spiral, out Spiral))
            {
                return BadRequest("Error in PressureCollapseController, Spiral onvert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.Diameter, out Diameter))
            {
                return BadRequest("Error in PressureCollapseController, Diameter convert failed.");
            }

            decimal DuctTemp;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.DuctTemp, out DuctTemp))
            {
                return BadRequest("Error in PressureCollapseController, DuctTemp convert failed.");
            }

            decimal CollapsePressure;
            CollapsePressure = AirFlowTechTools.CalcNegativePressureNoStiffeners(operatingPressure.Material, Gauge, Spiral, Diameter, DuctTemp);
            operatingPressure.OperatingPressure = 0;
            operatingPressure.Pressure = CollapsePressure;
            
            return Ok(operatingPressure);
        }
    }
}
