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
    public class AcousticalController : Controller
    {
        // POST api/acoustical/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Acoustical acoustical )
        {
            int Levels;
            if ( !UMCLib.TryConvertToInt32(acoustical.Levels, out Levels))
            {
                return BadRequest("Error in AcousticalController, Levels convert failed.");
            }

            double LengthBefore;
            if ( !UMCLib.TryConvertToDouble(acoustical.LengthBefore, out LengthBefore))
            {
                return BadRequest("Error in AcousticalController, LengthBefore convert failed.");
            }

            double LengthAfter;
            if ( !UMCLib.TryConvertToDouble(acoustical.LengthAfter, out LengthAfter))
            {
                return BadRequest("Error in AcousticalController, LengthAfter convert failed.");
            }

            double Distance;
            if ( !UMCLib.TryConvertToDouble(acoustical.Distance, out Distance))
            {
                return BadRequest("Error in AcousticalController, Distance convert failed.");
            }

            try 
            {
                int[,] OutputLevels;
                double Overall;

                // Pass Levels istead of looking at array

                AirFlowTechTools.CalcAcoustical(acoustical.CalcType,acoustical.Levels, acoustical.InputLevels,LengthBefore,LengthAfter,Distance,out OutputLevels,out Overall);
                
                decimal returnOverall;
                if ( !UMCLib.TryConvertToDecimal(Overall,out returnOverall))
                {
                    return BadRequest("Error in AcousticalController, Overall convert failed.");
                }

                acoustical.Overall = returnOverall;
                acoustical.OutputLevels = OutputLevels;
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }

            acoustical.OutputDesc = "Sound at Distance"; // Discharge
            if ( acoustical.CalcType == "AWeighting")
            {
                acoustical.OutputDesc = "Low A Levels";
            }
            else if ( acoustical.CalcType == "Addition")
            {
                acoustical.OutputDesc = "Low Addition";
            }
            
           
            return Ok(acoustical);
        }
    }
}
