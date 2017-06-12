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
    public class DDFController : Controller
    {
        // POST api/ddf/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] DDF ddf )
        {
            int VelocityRange;
            if ( !UMCLib.TryConvertToInt32(ddf.VelocityRange, out VelocityRange))
            {
                return BadRequest("Error in DDFController, VelocityRange convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(ddf.Diameter, out Diameter))
            {
                return BadRequest("Error in DDFController, Diameter convert failed.");
            }

            bool ConstDiameter;
            if ( !UMCLib.TryConvertToBoolean(ddf.ConstDiameter, out ConstDiameter))
            {
                return BadRequest("Error in DDFController, ConstDiameter convert failed.");
            }

            decimal CFM;
            if ( !UMCLib.TryConvertToDecimal(ddf.CFM, out CFM))
            {
                return BadRequest("Error in DDFController, CFM convert failed.");
            }

            decimal Length;
            if ( !UMCLib.TryConvertToDecimal(ddf.Length, out Length))
            {
                return BadRequest("Error in DDFController, Length convert failed.");
            }

            try 
            {
                List<string> ductList;
                List<string> orificeList;
                int EnteringVelocity;
                decimal TotalPressureDrop;
                string Error;

                Error = AirFlowTechTools.CalcDuctDFuser(VelocityRange,Diameter,ConstDiameter,CFM,Length,out ductList, out orificeList, out EnteringVelocity, out TotalPressureDrop);

                ddf.EnteringVelocity = EnteringVelocity;
                ddf.TotalPressureDrop = TotalPressureDrop;
                ddf.DuctList = ductList;
                ddf.OrificeList = orificeList;
                ddf.Error = Error;
                
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
           
            return Ok(ddf);
        }
    }
}
