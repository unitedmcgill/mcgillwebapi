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
    public class ReinforcementController : Controller
    {
        // POST api/reinforcement/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Reinforcement reinforcement )
        {
            decimal Minor;
            if ( !UMCLib.TryConvertToDecimal(reinforcement.Minor, out Minor))
            {
                return BadRequest("Error in ReinforcementController, Minor convert failed.");
            }

            decimal Major;
            if ( !UMCLib.TryConvertToDecimal(reinforcement.Major, out Major))
            {
                return BadRequest("Error in ReinforcementController, Major convert failed.");
            }

            int Gauge;
            if ( !UMCLib.TryConvertToInt32(reinforcement.Gauge, out Gauge))
            {
                return BadRequest("Error in ReinforcementController, Gauge convert failed.");
            }

            int SelectedReinforcement;
            if ( !UMCLib.TryConvertToInt32(reinforcement.SelectedReinforcement, out SelectedReinforcement))
            {
                return BadRequest("Error in ReinforcementController, SelectedReinforcement convert failed.");
            }

            int CalculatedGauge;
            if ( !UMCLib.TryConvertToInt32(reinforcement.CalculatedGauge, out CalculatedGauge))
            {
                return BadRequest("Error in ReinforcementController, CalculatedGauge convert failed.");
            }

            try 
            {
                string MinorReinforcement;
                string MajorReinforcement;
                
                AirFlowTechTools.CalcOvalRect(reinforcement.DuctType, reinforcement.CalcType, reinforcement.PressureClass, Minor, Major, Gauge, reinforcement.SelectedReinforcement, reinforcement.Application, out MinorReinforcement, out MajorReinforcement, out CalculatedGauge);

                reinforcement.MinorReinforcement = MinorReinforcement;
                reinforcement.MajorReinforcement = MajorReinforcement;
                reinforcement.CalculatedGauge = CalculatedGauge;
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
           
            return Ok(reinforcement);
        }
    }
}
