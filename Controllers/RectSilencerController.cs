using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using McGill.Web;
using McGill.Library;
using McGillWebAPI.Models;
using McGillWebAPI.Model;

namespace McGillWebAPI.Controllers
{   
    [Route("api/[controller]")]
    public class RectSilencerController : Controller
    {   
        private static int RetangularSort(RectResult a, RectResult b)
        {
            int nResult = -1 * a.Acceptable.CompareTo(b.Acceptable);

            if (nResult != 0)
            {
                return nResult;
            }

            return a.Weight.CompareTo(b.Weight);
        }        
        private static double RoundPD(double dPressureDrop)
        {
            dPressureDrop = Math.Floor(dPressureDrop * 1000) / 1000;

            if (dPressureDrop.ToString().EndsWith("5"))
            {
                return UMCLib.Round(dPressureDrop - .001, 2);
            }
            else
            {
                return UMCLib.Round(dPressureDrop, 2);
            }
        }

        // POST api/rectsilencer/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RectSilencerInput rectsilencer )
        {
            try 
            {
                double dCalcVelocity;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.CalcVelocity, out dCalcVelocity))
                {
                    return BadRequest("Error in RectSilencerInput, CalcVelocity convert failed.");
                }

                double dDisplayVelocity;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.DisplayVelocity, out dDisplayVelocity))
                {
                    return BadRequest("Error in RectSilencerInput, DisplayVelocity convert failed.");
                }
                
                double dCFM;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.CFM, out dCFM))
                {
                    return BadRequest("Error in RectSilencerInput, CFM convert failed.");
                }
                
                double dHeight;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Height, out dHeight))
                {
                    return BadRequest("Error in RectSilencerInput, Height convert failed.");
                }

                double dWidth;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Width, out dWidth))
                {
                    return BadRequest("Error in RectSilencerInput, Width convert failed.");
                }
                
                double calcVelocity = 0;
                // calculate the velocity (this is no longer done in the GUI)
                if ( (dWidth*dHeight)!=0){
                    calcVelocity = dCFM / ((dHeight*dWidth)/144);
                }

                dCalcVelocity = Math.Round(calcVelocity,3);
                dDisplayVelocity = Math.Round(calcVelocity,0);

                // Update the UI with the calculated velocity
                decimal decCalcVelocity = 0;
                UMCLib.TryConvertToDecimal(dCalcVelocity, out decCalcVelocity);
                rectsilencer.CalcVelocity = decCalcVelocity;

                double dPressureDrop;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.PressureDrop, out dPressureDrop))
                {
                    return BadRequest("Error in RectSilencerInput, PressureDrop convert failed.");
                }

                double dOne;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq1, out dOne))
                {
                    return BadRequest("Error in RectSilencerInput, Freq1 convert failed.");
                }
                
                double dTwo;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq2, out dTwo))
                {
                    return BadRequest("Error in RectSilencerInput, Freq2 convert failed.");
                }

                double dThree;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq3, out dThree))
                {
                    return BadRequest("Error in RectSilencerInput, Freq3 convert failed.");
                }

                double dFour;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq4, out dFour))
                {
                    return BadRequest("Error in RectSilencerInput, Freq4 convert failed.");
                }

                double dFive;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq5, out dFive))
                {
                    return BadRequest("Error in RectSilencerInput, Freq5 convert failed.");
                }

                double dSix;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq6, out dSix))
                {
                    return BadRequest("Error in RectSilencerInput, Freq6 convert failed.");
                }

                double dSeven;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq7, out dSeven))
                {
                    return BadRequest("Error in RectSilencerInput, Freq7 convert failed.");
                }

                double dEight;
                if ( !UMCLib.TryConvertToDouble(rectsilencer.Freq8, out dEight))
                {
                    return BadRequest("Error in RectSilencerInput, Freq8 convert failed.");
                }

                // Read the database
                AirSilenceContext context = new AirSilenceContext();
                
                Func<RectSilencer,bool> whereClause = r => r.Velocity >= 0;
                if ( dCalcVelocity < 0 )
                {
                    whereClause = r => r.Velocity <= 0;
                }

                var rawResult = context.RectSilencer.Where(whereClause)
                    .OrderBy(r => r.Model)
                    .ThenBy(r => r.Length)
                    .ThenBy(r => r.Velocity);

                List<RectSilencer> rawData = new List<RectSilencer>();
                rawData = rawResult.ToList();
                List<RectResult> interpolation = new List<RectResult>();

                for (int i = 0; i < rawData.Count; i += 2)
                {
                    RectSilencer startData = rawData[i];
                    RectSilencer endData = rawData[i + 1];

                    RectResult interData = new RectResult();
                    interData.Model = startData.Model;
                    interData.Length = startData.Length;
                    interData.Velocity = UMCLib.ConvertToInt32(dCalcVelocity);

                    interData.FreqOne = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqOne - startData.FreqOne) + startData.FreqOne, 0);
                    interData.FreqTwo = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqTwo - startData.FreqTwo) + startData.FreqTwo, 0);
                    interData.FreqThree = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqThree - startData.FreqThree) + startData.FreqThree, 0);
                    interData.FreqFour = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqFour - startData.FreqFour) + startData.FreqFour, 0);
                    interData.FreqFive = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqFive - startData.FreqFive) + startData.FreqFive, 0);
                    interData.FreqSix = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqSix - startData.FreqSix) + startData.FreqSix, 0);
                    interData.FreqSeven = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqSeven - startData.FreqSeven) + startData.FreqSeven, 0);
                    interData.FreqEight = UMCLib.Round(((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqEight - startData.FreqEight) + startData.FreqEight, 0);

                    interData.PressureDrop = RoundPD(startData.LossCoef.Value * Math.Pow(dCalcVelocity / 4005.0D, 2));
                    interData.Weight = UMCLib.Round(dHeight * dWidth * startData.Length / 144.0D * startData.WeightFactor.Value, 0);
                    interData.Acceptable = 0;
                    interData.Type = startData.Type;

                    // Default Selected to false
                    interData.Selected = 0;

                    interpolation.Add(interData);
                }

                for (int i = 0; i < interpolation.Count; i++)
                {
                    bool bBadPD = false;
                    bool bAlmostPD = false;
                    bool bAcceptablePD = false;
                    bool bBadAcoustics = false;
                    bool bAlmostAcoustics = false;
                    bool bAcceptableAcoustics = false;

                    RectResult interData = interpolation[i];

                    if (interData.PressureDrop <= dPressureDrop * 1.5D)
                    {
                        bBadPD = true;

                        if (interData.PressureDrop <= dPressureDrop * 1.2D)
                        {
                            bAlmostPD = true;

                            if (interData.PressureDrop <= dPressureDrop * 1.1D)
                            {
                                bAcceptablePD = true;
                            }
                        }
                    }

                    if (interData.FreqOne + 7.0D >= dOne && interData.FreqTwo + 7.0D >= dTwo && interData.FreqThree + 7.0D >= dThree && interData.FreqFour + 7.0D >= dFour &&
                        interData.FreqFive + 7.0D >= dFive && interData.FreqSix + 7.0D >= dSix && interData.FreqSeven + 7.0D >= dSeven && interData.FreqEight + 7.0D >= dEight)
                    {
                        bBadAcoustics = true;

                        if (interData.FreqOne + 6.0D >= dOne && interData.FreqTwo + 3.0D >= dTwo && interData.FreqThree + 3.0D >= dThree && interData.FreqFour + 3.0D >= dFour &&
                            interData.FreqFive + 3.0D >= dFive && interData.FreqSix + 3.0D >= dSix && interData.FreqSeven + 3.0D >= dSeven && interData.FreqEight + 3.0D >= dEight)
                        {
                            bAlmostAcoustics = true;

                            if (interData.FreqOne >= dOne && interData.FreqTwo + .5D >= dTwo && interData.FreqThree + .5D >= dThree && interData.FreqFour + .5D >= dFour &&
                                interData.FreqFive + .5D >= dFive && interData.FreqSix + .5D >= dSix && interData.FreqSeven + .5D >= dSeven && interData.FreqEight + .5D >= dEight)
                            {
                                bAcceptableAcoustics = true;
                            }
                        }
                    }

                    if (bBadPD && bBadAcoustics)
                    {
                        interData.Acceptable = 1;

                        if (bAlmostPD || bAlmostAcoustics)
                        {
                            interData.Acceptable = 2;

                            if (bAlmostPD && bAlmostAcoustics)
                            {
                                interData.Acceptable = 3;

                                if (bAcceptablePD || bAcceptableAcoustics)
                                {
                                    interData.Acceptable = 4;

                                    if (bAcceptablePD && bAcceptableAcoustics)
                                    {
                                        interData.Acceptable = 5;
                                    }
                                }
                            }
                        }
                    }
                }

                rectsilencer.Silencers = new List<RectResult>();
                for (int i = 0; i < interpolation.Count; i++)
                {
                    RectResult interData = interpolation[i];

                    if (!String.IsNullOrEmpty(rectsilencer.Type) && interData.Type != rectsilencer.Type)
                    {
                        // If filtering based on silencer type and not a match, skip it
                        continue;
                    }

                    switch(interData.Acceptable)
                    {
                        case 5:
                            rectsilencer.Silencers.Add(interData);
                            break;
                        case 4:
                        case 3:
                        case 2:
                        case 1:
                            if (interData.FreqOne < dOne)
                            {
                                interData.FreqOne *= -1.0D;
                            }
                            if (interData.FreqTwo + .5D < dTwo)
                            {
                                interData.FreqTwo *= -1.0D;
                            }
                            if (interData.FreqThree + .5D < dThree)
                            {
                                interData.FreqThree *= -1.0D;
                            }
                            if (interData.FreqFour + .5D < dFour)
                            {
                                interData.FreqFour *= -1.0D;
                            }
                            if (interData.FreqFive + .5D < dFive)
                            {
                                interData.FreqFive *= -1.0D;
                            }
                            if (interData.FreqSix + .5D < dSix)
                            {
                                interData.FreqSix *= -1.0D;
                            }
                            if (interData.FreqSeven + .5D < dSeven)
                            {
                                interData.FreqSeven *= -1.0D;
                            }
                            if (interData.FreqEight + .5D < dEight)
                            {
                                interData.FreqEight *= -1.0D;
                            }
                            if(interData.PressureDrop > dPressureDrop * 1.1D)
                            {
                                interData.PressureDrop *= -1.0D;
                            }
                            rectsilencer.Silencers.Add(interData);
                            break;
                        default:
                            break;
                    }
                }

                rectsilencer.Silencers.Sort(RetangularSort);
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
         
            return Ok(rectsilencer);
        }
    }
}
