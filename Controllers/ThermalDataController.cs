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
    public class ThermalDataController : Controller
    {
        // POST api/thermaldata/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] ThermalData thermaldata )
        {
            decimal Insulation;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.Insulation, out Insulation))
            {
                return BadRequest("Error in ThermalDataController, Insulation convert failed.");
            }

            int Wind;
            if ( !UMCLib.TryConvertToInt32(thermaldata.Wind, out Wind))
            {
                return BadRequest("Error in ThermalDataController, Wind convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.Diameter, out Diameter))
            {
                return BadRequest("Error in ThermalDataController, Diameter convert failed.");
            }

            decimal FlowRate;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.FlowRate, out FlowRate))
            {
                return BadRequest("Error in ThermalDataController, FlowRate convert failed.");
            }

            int InsideDuctTemp;
            if ( !UMCLib.TryConvertToInt32(thermaldata.InsideDuctTemp, out InsideDuctTemp))
            {
                return BadRequest("Error in ThermalDataController, InsideDuctTemp convert failed.");
            }

            decimal AmbientTemp;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.AmbientTemp, out AmbientTemp))
            {
                return BadRequest("Error in ThermalDataController, AmbientTemp convert failed.");
            }

            decimal DuctLength;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.DuctLength, out DuctLength))
            {
                return BadRequest("Error in ThermalDataController, DuctLength convert failed.");
            }

            decimal Humidity;
            if ( !UMCLib.TryConvertToDecimal(thermaldata.Humidity, out Humidity))
            {
                return BadRequest("Error in ThermalDataController, Humidity convert failed.");
            }

            decimal HeatTransfer;
            decimal SkinTemp;
            decimal ExitTemp;
            decimal DewpointTemp;
            decimal Conductivity;
            decimal Density;
            string Condensation;

            try 
            {
                AirFlowTechTools.CalcThermalData(Insulation, Wind, Diameter ,Humidity, FlowRate, InsideDuctTemp, AmbientTemp, DuctLength, out HeatTransfer, out SkinTemp, out ExitTemp, out DewpointTemp, out Condensation, out Conductivity, out Density);
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }

            thermaldata.HeatTransfer = HeatTransfer;
            thermaldata.SkinTemp = SkinTemp;
            thermaldata.ExitTemp = ExitTemp;
            thermaldata.DewpointTemp = DewpointTemp;
            thermaldata.Conductivity = Conductivity;
            thermaldata.Density = Density;
            thermaldata.Condensation = Condensation;
            
            return Ok(thermaldata);
        }
    }
}
