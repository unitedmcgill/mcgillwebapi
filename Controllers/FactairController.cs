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
    public class FactairController : Controller
    {
        // POST api/factair/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Factair factair )
        {
            int Model;
            if ( !UMCLib.TryConvertToInt32(factair.Model, out Model))
            {
                return BadRequest("Error in FactairController, Model convert failed.");
            }

            decimal Velocity;
            if ( !UMCLib.TryConvertToDecimal(factair.Velocity, out Velocity))
            {
                return BadRequest("Error in FactairController, Velocity convert failed.");
            }

            int Distance;
            if ( !UMCLib.TryConvertToInt32(factair.Distance, out Distance))
            {
                return BadRequest("Error in FactairController, Distance convert failed.");
            }

            bool Position = false;
            if ( String.Equals(factair.Position,"Open"))
            {
                Position = true;
            }

            try 
            {
                decimal MaxVelocity;
                decimal PressureDrop;
                decimal[] Octaves;

                AirFlowTechTools.CalcFactair(Model,Velocity,Position,Distance,out MaxVelocity, out PressureDrop, out Octaves);

                factair.MaxVelocity = MaxVelocity;
                factair.PressureDrop = PressureDrop;
                factair.Octaves = Octaves;
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
           
            return Ok(factair);
        }
    }
}
