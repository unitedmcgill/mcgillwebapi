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
    public class ConvertDuctTypesController : Controller
    {
        // POST api/convertductypes/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] DuctConvert ductConvert )
        {
            //Contact contact = new Contact();
            ductConvert.Name = "testing";

            // Fix enums
            DuctType duct = (DuctType)Convert.ToInt32(ductConvert.Type);

            //if ( duct == DuctType.Oval)
            //{
            //    test.Type = DuctType.Rectangular;
            //}

            string sCalcType = duct.ToString();

            // The function looks for Rect instead of Rectangular
            if ( sCalcType == "Rectangular" )
            {
                sCalcType = sCalcType.Substring(0,4);
            }

            decimal RectMinor;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.RectMinor, out RectMinor))
            {
                return BadRequest("Error in ConvertDuctTypeController, RectMinor convert failed.");
            }

            decimal RectMajor;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.RectMajor, out RectMajor))
            {
                return BadRequest("Error in ConvertDuctTypeController, RectMajor convert failed.");
            }

            decimal OvalMinor;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.OvalMinor, out OvalMinor))
            {
                return BadRequest("Error in ConvertDuctTypeController, OvalMinor convert failed.");
            }

            decimal OvalMajor;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.OvalMajor, out OvalMajor))
            {
                return BadRequest("Error in ConvertDuctTypeController, OvalMajor convert failed.");
            }

            decimal Diameter;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.Diameter, out Diameter))
            {
                return BadRequest("Error in ConvertDuctTypeController, Diameter convert failed.");
            }

            decimal Minor;
            if ( !UMCLib.TryConvertToDecimal(ductConvert.Minor, out Minor))
            {
                return BadRequest("Error in ConvertDuctTypeController, Minor convert failed.");
            }

            // CalcConversion(string sCalcType, decimal mRectMinor, decimal mRectMajor, decimal mOvalMinor, 
            // decimal mOvalMajor, decimal mDiamter, decimal mMinor,out decimal mResult1, out decimal mResult2)
            decimal Result1;
            decimal Result2;
            AirFlowTechTools.CalcConversion( sCalcType, RectMinor, RectMajor, OvalMinor, OvalMajor, Diameter, Minor, out Result1, out Result2 );
            ductConvert.Result1 = Result1;
            ductConvert.Result2 = Result2;
            
            return Ok(ductConvert);
        }
    }
}
