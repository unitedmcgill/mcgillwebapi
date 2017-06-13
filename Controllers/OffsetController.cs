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
    public class OffsetController : Controller
    {
        // POST api/offset/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Offset offset )
        {
            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(offset.Diameter, out Diameter))
            {
                return BadRequest("Error in OffsetController, Diameter convert failed.");
            }

            decimal Distance;
            if ( !UMCLib.TryConvertToDecimal(offset.Distance, out Distance))
            {
                return BadRequest("Error in OffsetController, Distance convert failed.");
            }

            decimal Length;
            if ( !UMCLib.TryConvertToDecimal(offset.Length, out Length))
            {
                return BadRequest("Error in OffsetController, Length convert failed.");
            }

            decimal CenterLine1;
            if ( !UMCLib.TryConvertToDecimal(offset.CenterLine1, out CenterLine1))
            {
                return BadRequest("Error in OffsetController, CenterLine1 convert failed.");
            }

            decimal CenterLine2;
            if ( !UMCLib.TryConvertToDecimal(offset.CenterLine2, out CenterLine2))
            {
                return BadRequest("Error in OffsetController, CenterLine2 convert failed.");
            }

            decimal Angle;
            if ( !UMCLib.TryConvertToDecimal(offset.Angle, out Angle))
            {
                return BadRequest("Error in OffsetController, Angle convert failed.");
            }

            try 
            {
                decimal CalcLength;

                AirFlowTechTools.CalcOffset(offset.CalcType, offset.ConnectionType, Diameter, Length, CenterLine1, CenterLine2, ref Distance, ref Angle, out CalcLength);

                offset.Distance = Distance;
                offset.Angle = Angle;
                offset.CalcLength = CalcLength.ToString();

                offset.CalcLengthDesc = "Overall Length (in)";
                if ( offset.CalcType == "Duct")
                {
                    offset.CalcLengthDesc = "Duct Length (in)";
                }
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
           
            return Ok(offset);
        }
    }
}
