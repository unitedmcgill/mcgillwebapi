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
    public class SupportController : Controller
    {
        // POST api/support/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Support support )
        {
            decimal Insulation;
            if ( !UMCLib.TryConvertToDecimal(support.Insulation, out Insulation))
            {
                return BadRequest("Error in SupportController, Insulation convert failed.");
            }

            decimal Load;
            if ( !UMCLib.TryConvertToDecimal(support.Load, out Load))
            {
                return BadRequest("Error in SupportController, Load convert failed.");
            }
            
            decimal Density;
            if ( !UMCLib.TryConvertToDecimal(support.Density, out Density))
            {
                return BadRequest("Error in SupportController, Density convert failed.");
            }

            decimal SafetyFactor;
            if ( !UMCLib.TryConvertToDecimal(support.SafetyFactor, out SafetyFactor))
            {
                return BadRequest("Error in SupportController, SafetyFactor convert failed.");
            }

            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(support.Spiral, out Spiral))
            {
                return BadRequest("Error in SupportController, Spiral convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(support.Diameter, out Diameter))
            {
                return BadRequest("Error in SupportController, Diameter convert failed.");
            }
            
            decimal Wind;
            if ( !UMCLib.TryConvertToDecimal(support.Wind, out Wind))
            {
                return BadRequest("Error in SupportController, Wind convert failed.");
            }

            decimal Snow;
            if ( !UMCLib.TryConvertToDecimal(support.Snow, out Snow))
            {
                return BadRequest("Error in SupportController, Snow convert failed.");
            }

            decimal RingSpacing; // Stiffner
            if ( !UMCLib.TryConvertToDecimal(support.RingSpacing, out RingSpacing))
            {
                return BadRequest("Error in SupportController, RingSpacing convert failed.");
            }

            int InnerGauge;
            if ( !UMCLib.TryConvertToInt32(support.InnerGauge, out InnerGauge))
            {
                return BadRequest("Error in SupportController, InnerGauge convert failed.");
            }

            int OuterGauge;
            if ( !UMCLib.TryConvertToInt32(support.OuterGauge, out OuterGauge))
            {
                return BadRequest("Error in SupportController, OuterGauge convert failed.");
            }

            decimal MaterialLoad;
            decimal AllowedDeflection;
            decimal ActualDeflection;
            decimal MaxLength;

            try
            {
                if ( Insulation == 0 )
                {
                    support.PassFail = AirFlowTechTools.CalcSupport(support.Material, InnerGauge, Spiral, Diameter, RingSpacing, Load, Density, Wind, Snow, SafetyFactor, out MaterialLoad, out AllowedDeflection, out ActualDeflection, out MaxLength);
                }
                else 
                {
                    support.PassFail = AirFlowTechTools.CalcSupport( Insulation, support.Material, InnerGauge, OuterGauge, Spiral, Diameter, RingSpacing, Load, Density, Wind, Snow, SafetyFactor, out MaterialLoad, out AllowedDeflection, out ActualDeflection, out MaxLength);
                }
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }

            support.MaterialLoad = MaterialLoad;
            support.AllowedDeflection = AllowedDeflection;
            support.ActualDeflection = ActualDeflection;
            support.MaxLength = MaxLength;
            
            return Ok(support);
        }
    }
}
