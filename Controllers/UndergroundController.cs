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
    public class UndergroundController : Controller
    {
        // POST api/underground/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Underground underground )
        {
            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(underground.Spiral, out Spiral))
            {
                return BadRequest("Error in UndergroundController, Spiral convert failed.");
            }

            decimal DistLoad;
            if ( !UMCLib.TryConvertToDecimal(underground.DistLoad, out DistLoad))
            {
                return BadRequest("Error in UndergroundController, DistLoad convert failed.");
            }

            decimal Vehicle;
            if ( !UMCLib.TryConvertToDecimal(underground.Vehicle, out Vehicle))
            {
                return BadRequest("Error in UndergroundController, DistLoad convert failed.");
            }

            decimal Area;
            if ( !UMCLib.TryConvertToDecimal(underground.Area, out Area))
            {
                return BadRequest("Error in UndergroundController, Area convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(underground.Diameter, out Diameter))
            {
                return BadRequest("Error in UndergroundController, Diameter convert failed.");
            }

            int Gauge;
            if ( !UMCLib.TryConvertToInt32(underground.Gauge, out Gauge))
            {
                return BadRequest("Error in UndergroundController, Gauge convert failed.");
            }

            decimal Density;
            if ( !UMCLib.TryConvertToDecimal(underground.Density, out Density))
            {
                return BadRequest("Error in UndergroundController, Density convert failed.");
            }

            decimal Depth;
            if ( !UMCLib.TryConvertToDecimal(underground.Depth, out Depth))
            {
                return BadRequest("Error in UndergroundController, Depth convert failed.");
            }

            decimal Modulus;
            if ( !UMCLib.TryConvertToDecimal(underground.Modulus, out Modulus))
            {
                return BadRequest("Error in UndergroundController, Modulus convert failed.");
            }

            decimal SoilLoad;
            decimal ExternalLoad;
            decimal TotalLoad;
            decimal Deflection;
            decimal MaxDepth;

            try 
            {
                underground.PassFail = AirFlowTechTools.CalcUnderground(underground.LoadType, underground.Material, Gauge, Spiral, Diameter, DistLoad, Vehicle, Area, Density, Depth, Modulus, out SoilLoad, out ExternalLoad, out TotalLoad, out Deflection, out MaxDepth);
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }

            underground.SoilLoad = SoilLoad;
            underground.ExternalLoad = ExternalLoad;
            underground.Deflection = Deflection;
            underground.TotalLoad = TotalLoad;
            underground.MaxDepth = MaxDepth;
            
            return Ok(underground);
        }
    }
}
