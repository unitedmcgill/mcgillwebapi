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
    public class OrificeTubeController : Controller
    {
        // POST api/orificetube/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] OrificeTube orificetube )
        {
            decimal TestPressure;
            if ( !UMCLib.TryConvertToDecimal(orificetube.TestPressure, out TestPressure))
            {
                return BadRequest("Error in OrificeTubeController, TestPressure convert failed.");
            }

            decimal CFM;
            if ( !UMCLib.TryConvertToDecimal(orificetube.CFM, out CFM))
            {
                return BadRequest("Error in OrificeTubeController, CFM convert failed.");
            }

            decimal TubeDiameter;
            if ( !UMCLib.TryConvertToDecimal(orificetube.TubeDiameter, out TubeDiameter))
            {
                return BadRequest("Error in OrificeTubeController, TubeDiameter convert failed.");
            }

            decimal OrificeDiameter;
            if ( !UMCLib.TryConvertToDecimal(orificetube.OrificeDiameter, out OrificeDiameter))
            {
                return BadRequest("Error in OrificeTubeController, OrificeDiameter convert failed.");
            }

            try 
            {
                decimal BetaRatio;
                string OpenArea;
                decimal[,] TubeList;

                TubeList = AirFlowTechTools.CalcOrificeTube(TestPressure, CFM, orificetube.Plate, TubeDiameter, OrificeDiameter, out BetaRatio, out OpenArea);

                orificetube.BetaRatio = BetaRatio;
                orificetube.OpenArea = OpenArea;
                orificetube.TubeList = TubeList;
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
           
            return Ok(orificetube);
        }
    }
}
