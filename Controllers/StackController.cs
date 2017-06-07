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
    public class StackController : Controller
    {
        // POST api/stack/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Stack stack )
        {
            decimal SafetyFactor;
            if ( !UMCLib.TryConvertToDecimal(stack.SafetyFactor, out SafetyFactor))
            {
                return BadRequest("Error in StackController, SafetyFactor convert failed.");
            }

            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(stack.Spiral, out Spiral))
            {
                return BadRequest("Error in StackController, Spiral convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(stack.Diameter, out Diameter))
            {
                return BadRequest("Error in StackController, Diameter convert failed.");
            }

            decimal Wind;
            if ( !UMCLib.TryConvertToDecimal(stack.Wind, out Wind))
            {
                return BadRequest("Error in StackController, Wind convert failed.");
            }

            decimal Height;
            if ( !UMCLib.TryConvertToDecimal(stack.Height, out Height))
            {
                return BadRequest("Error in StackController, Height convert failed.");
            }

            int Gauge;
            if ( !UMCLib.TryConvertToInt32(stack.Gauge, out Gauge))
            {
                return BadRequest("Error in StackController, Gauge convert failed.");
            }

            decimal Velocity;
            decimal Stress;
            decimal Deflection;
            decimal Yield;
            decimal Buckling;

            try 
            {
                stack.PassFail = AirFlowTechTools.CalcStack(stack.Material, Gauge, Spiral, Diameter, Wind, SafetyFactor, Height, out Velocity, out Stress, out Buckling, out Yield, out Deflection);
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
            
            stack.Velocity = Velocity;
            stack.Stress = Stress;
            stack.Deflection = Deflection;
            stack.Yield = Yield;
            stack.Buckling = Buckling;
            
            return Ok(stack);
        }
    }
}
