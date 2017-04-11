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
    public class MinThicknessController : Controller
    {
        // POST api/minthickness/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] CalcOperatingPressure operatingPressure )
        {
            int Gauge;
            // if ( !UMCLib.TryConvertToInt32(operatingPressure.Gauge, out Gauge))
            // {
            //     return BadRequest("Error in PressureController, Gauge convert failed.");
            // }

            bool Spiral;
            if ( !UMCLib.TryConvertToBoolean(operatingPressure.Spiral, out Spiral))
            {
                return BadRequest("Error in MinThicknessController, Spiral onvert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.Diameter, out Diameter))
            {
                return BadRequest("Error in MinThicknessController, Diameter convert failed.");
            }

            decimal StiffenerSpacing;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.StiffenerSpacing, out StiffenerSpacing))
            {
                return BadRequest("Error in MinThicknessController, StiffenerSpacing convert failed.");
            }

            decimal DuctTemp;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.DuctTemp, out DuctTemp))
            {
                return BadRequest("Error in MinThicknessController, DuctTemp convert failed.");
            }

            decimal Pressure;
            if ( !UMCLib.TryConvertToDecimal(operatingPressure.Pressure, out Pressure))
            {
                return BadRequest("Error in MinThicknessController, Pressure convert failed.");
            }

            // CalcConversion(string sCalcType, decimal mRectMinor, decimal mRectMajor, decimal mOvalMinor, 
            // decimal mOvalMajor, decimal mDiamter, decimal mMinor,out decimal mResult1, out decimal mResult2)
            decimal Thickness;
            string StiffenerSize;
            Thickness = AirFlowTechTools.CalcMinThickness( operatingPressure.Material, Spiral, Diameter, StiffenerSpacing, Pressure, DuctTemp, operatingPressure.DuctClass, out Gauge, out StiffenerSize );
            operatingPressure.OperatingPressure = Thickness;
            operatingPressure.Gauge = Gauge;
            operatingPressure.StiffenerSize = StiffenerSize;
            
            return Ok(operatingPressure);
        }
    }
}
