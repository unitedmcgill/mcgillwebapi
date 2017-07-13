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
    public class RoundSilencerController : Controller
    {   
        private static int RoundSort(SilencerResult a, SilencerResult b)
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

        // POST api/roundsilencer/
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RoundSilencerInput roundsilencer )
        {
            try 
            {
                double dCalcVelocity;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.CalcVelocity, out dCalcVelocity))
                {
                    return BadRequest("Error in RoundSilencerInput, CalcVelocity convert failed.");
                }

                double dDisplayVelocity;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.DisplayVelocity, out dDisplayVelocity))
                {
                    return BadRequest("Error in RoundSilencerInput, DisplayVelocity convert failed.");
                }
                
                double dCFM;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.CFM, out dCFM))
                {
                    return BadRequest("Error in RoundSilencerInput, CFM convert failed.");
                }
                
                double dDiameter;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Diameter, out dDiameter))
                {
                    return BadRequest("Error in RoundSilencerInput, Diameter convert failed.");
                }
               
                double calcVelocity = 0;
                // calculate the velocity (this is no longer done in the GUI)
                if ( (dDiameter)!=0){
                    calcVelocity = dCFM / (3.14159 * dDiameter * dDiameter / 4 / 144);
                }

                dCalcVelocity = Math.Round(calcVelocity,3);
                dDisplayVelocity = Math.Round(calcVelocity,0);

                // Update the UI with the calculated velocity
                decimal decCalcVelocity = 0;
                UMCLib.TryConvertToDecimal(dCalcVelocity, out decCalcVelocity);
                roundsilencer.CalcVelocity = decCalcVelocity;

                double dPressureDrop;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.PressureDrop, out dPressureDrop))
                {
                    return BadRequest("Error in RoundSilencerInput, PressureDrop convert failed.");
                }

                double dOne;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq1, out dOne))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq1 convert failed.");
                }
                
                double dTwo;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq2, out dTwo))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq2 convert failed.");
                }

                double dThree;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq3, out dThree))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq3 convert failed.");
                }

                double dFour;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq4, out dFour))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq4 convert failed.");
                }

                double dFive;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq5, out dFive))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq5 convert failed.");
                }

                double dSix;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq6, out dSix))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq6 convert failed.");
                }

                double dSeven;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq7, out dSeven))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq7 convert failed.");
                }

                double dEight;
                if ( !UMCLib.TryConvertToDouble(roundsilencer.Freq8, out dEight))
                {
                    return BadRequest("Error in RoundSilencerInput, Freq8 convert failed.");
                }

                // Validate the input
                if ( dDiameter > 100 || dDiameter < 0 ) return BadRequest("Diameter limited to sizes between 4 and 100 inches inclusive.");
                if ( dPressureDrop < 0 ) return BadRequest("Pressure Drop cannot be negative.");
                if ( dOne < 0 ) return BadRequest("Frequency(1) cannot be negative.");
                if ( dTwo < 0 ) return BadRequest("Frequency(2) cannot be negative.");
                if ( dThree < 0 ) return BadRequest("Frequency(3) cannot be negative.");
                if ( dFour < 0 ) return BadRequest("Frequency(4) cannot be negative.");
                if ( dFive < 0 ) return BadRequest("Frequency(5) cannot be negative.");
                if ( dSix < 0 ) return BadRequest("Frequency(6) cannot be negative.");
                if ( dSeven < 0 ) return BadRequest("Frequency(7) cannot be negative.");
                if ( dEight < 0 ) return BadRequest("Frequency(8) cannot be negative.");


                // Read the database
                AirSilenceContext context = new AirSilenceContext();
                
                Func<RoundSilencer,bool> whereClause = r => r.Velocity >= 0;
                if ( dCalcVelocity < 0 )
                {
                    whereClause = r => r.Velocity <= 0;
                }

                var rawResult = context.RoundSilencer.Where(whereClause)
                    .OrderBy(r => r.Model)
                    .ThenBy(r => r.Diameter)
                    .ThenBy(r => r.Length)
                    .ThenBy(r => r.Velocity);

                List<RoundSilencer> rawData = new List<RoundSilencer>();
                rawData = rawResult.ToList();
                List<SilencerResult> interpolation = new List<SilencerResult>();

                var roundResult = context.RoundSilencerExtraData
                    .OrderBy(r => r.Model);

                Dictionary<string, RoundSilencerExtraData> extraData = new Dictionary<string,RoundSilencerExtraData>();
                foreach ( RoundSilencerExtraData roundData in roundResult )
                {
                    if ( !extraData.ContainsKey(roundData.Model))
                    {
                        extraData.Add(roundData.Model, roundData);
                    }
                }

                for (int i = 0; i < rawData.Count; i += 2)
                {
                    RoundSilencer startData = rawData[i];
                    RoundSilencer endData = rawData[i + 1];

                    SilencerResult interData = new SilencerResult();
                    
                    interData.Model = startData.Model;
                    interData.Diameter = startData.Diameter;
                    interData.Length = startData.Length;
                    interData.Velocity = startData.Velocity;
                    interData.Weight = UMCLib.ConvertToDouble(startData.Weight);
                    interData.FreqOne = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqOne - startData.FreqOne) + startData.FreqOne;
                    interData.FreqTwo = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqTwo - startData.FreqTwo) + startData.FreqTwo;
                    interData.FreqThree = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqThree - startData.FreqThree) + startData.FreqThree;
                    interData.FreqFour = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqFour - startData.FreqFour) + startData.FreqFour;
                    interData.FreqFive = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqFive - startData.FreqFive) + startData.FreqFive;
                    interData.FreqSix = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqSix - startData.FreqSix) + startData.FreqSix;
                    interData.FreqSeven = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqSeven - startData.FreqSeven) + startData.FreqSeven;
                    interData.FreqEight = ((dCalcVelocity - startData.Velocity) / (endData.Velocity - startData.Velocity)) * (endData.FreqEight - startData.FreqEight) + startData.FreqEight;
                    interData.Type = startData.Type;

                    interpolation.Add(interData);
                }

                int nIncrement = 0;
                List<SilencerResult> secondInter = new List<SilencerResult>();

                for (int i = 0; i < interpolation.Count; i += nIncrement)
                {
                    SilencerResult interData = interpolation[i];
                    RoundSilencerExtraData roundData = extraData[interData.Model];

                    if (roundData.StaticLenDia == 1)
                    {
                        nIncrement = 1;

                        if (interData.Diameter == dDiameter)
                        {
                            interData.FreqOne = UMCLib.Round(interData.FreqOne, 0);
                            interData.FreqTwo = UMCLib.Round(interData.FreqTwo, 0);
                            interData.FreqThree = UMCLib.Round(interData.FreqThree, 0);
                            interData.FreqFour = UMCLib.Round(interData.FreqFour, 0);
                            interData.FreqFive = UMCLib.Round(interData.FreqFive, 0);
                            interData.FreqSix = UMCLib.Round(interData.FreqSix, 0);
                            interData.FreqSeven = UMCLib.Round(interData.FreqSeven, 0);
                            interData.FreqEight = UMCLib.Round(interData.FreqEight, 0);
                            interData.PressureDrop = RoundPD(roundData.NormLossCoef.Value * Math.Pow(dCalcVelocity / 4005.0D, 2));
                            secondInter.Add(interData);
                        }
                    }
                    else
                    {
                        nIncrement = roundData.NumberSizes.Value;

                        double dLength = 0;
                        switch (interData.Model)
                        {
                            case "CSFMVL25":
                            case "CSFHVL20":
                                dLength = dDiameter * 3.25D;
                                break;
                            case "CSFHVL45":
                                if (dDiameter < 18.0D)
                                {
                                    dLength = 36.0D;
                                }
                                else
                                {
                                    dLength = dDiameter * 2.0D;
                                }
                                break;
                            case "CSFHVL15":
                                dLength = dDiameter * 3.5D;
                                break;
                            case "CSFHVL39":
                                dLength = dDiameter * 2.0D;
                                break;
                            case "CSFHVL44":
                                if (dDiameter < 30.1D)
                                {
                                    dLength = dDiameter * 3.0D;
                                }
                                else
                                {
                                    dLength = dDiameter * 3.5D;
                                }
                                break;
                            case "CEFHVL20*":
                                dLength = 1.5D * dDiameter + 9.0D;
                                break;
                            case "CEFHVL55*":
                                if (dDiameter < 16.1D)
                                {
                                    dLength = 1.5D * (dDiameter + 6.0D) + 3.0D * dDiameter + 9.0D;
                                }
                                else
                                {
                                    dLength = 1.5D * (dDiameter + 6.0D) + 3.5D * dDiameter + 7.0D;
                                }
                                break;
                            default:
                                if (dDiameter < 16.0D)
                                {
                                    dLength = 32.0D;
                                }
                                else
                                {
                                    dLength = dDiameter * 2.0D;
                                }
                                break;
                        }

                        SilencerResult startData = null;
                        SilencerResult endData = null;

                        if((dDiameter < 12.1D && roundData.NumberSizes == 6) || (dDiameter < 24.1D & roundData.NumberSizes <= 5))
                        {
                            startData = interpolation[i];
                            endData = interpolation[i + 1];
                        }
                        else if ((dDiameter < 18.1D && roundData.NumberSizes == 6) || (dDiameter < 36.1D && roundData.NumberSizes <= 5))
                        {
                            startData = interpolation[i + 1];
                            endData = interpolation[i + 2];
                        }
                        else if ((dDiameter < 24.1D && roundData.NumberSizes == 6) || (dDiameter < 48.1D && roundData.NumberSizes == 5) || roundData.NumberSizes == 3)
                        {
                            startData = interpolation[i + 2];
                            endData = interpolation[i + 3];
                        }
                        else if ((dDiameter < 36.1D && roundData.NumberSizes == 6) || roundData.NumberSizes == 5)
                        {
                            startData = interpolation[i + 3];
                            endData = interpolation[i + 4];
                        }
                        else
                        {
                            startData = interpolation[i + 4];
                            endData = interpolation[i + 5];
                        }

                        interData.FreqOne = UMCLib.Round((startData.FreqOne - endData.FreqOne) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqOne,0);
                        interData.FreqTwo = UMCLib.Round((startData.FreqTwo - endData.FreqTwo) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqTwo, 0);
                        interData.FreqThree = UMCLib.Round((startData.FreqThree - endData.FreqThree) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqThree, 0);
                        interData.FreqFour = UMCLib.Round((startData.FreqFour - endData.FreqFour) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqFour, 0);
                        interData.FreqFive = UMCLib.Round((startData.FreqFive - endData.FreqFive) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqFive, 0);
                        interData.FreqSix = UMCLib.Round((startData.FreqSix - endData.FreqSix) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqSix, 0);
                        interData.FreqSeven = UMCLib.Round((startData.FreqSeven - endData.FreqSeven) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqSeven, 0);
                        interData.FreqEight = UMCLib.Round((startData.FreqEight - endData.FreqEight) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.FreqEight, 0);
                        
                        interData.PressureDrop = RoundPD(roundData.NormLossCoef.Value * Math.Pow(dCalcVelocity / 4005.0D, 2));
                        interData.Weight = UMCLib.Round((startData.Weight - endData.Weight) / (startData.Diameter - endData.Diameter) * (dDiameter - endData.Diameter) + endData.Weight, 0);
                        interData.Acceptable = 0;

                        secondInter.Add(interData);

                        if (roundData.TlossCoef > 0.0D)
                        {
                            SilencerResult tData = new SilencerResult();
                            tData.Model = interData.Model + "T";
                            tData.Diameter = interData.Diameter;
                            tData.Length = interData.Length;
                            tData.Velocity = interData.Velocity;

                            tData.FreqOne = interData.FreqOne;
                            tData.FreqTwo = interData.FreqTwo;
                            tData.FreqThree = interData.FreqThree;
                            tData.FreqFour = interData.FreqFour;
                            tData.FreqFive = interData.FreqFive;
                            tData.FreqSix = interData.FreqSix;
                            tData.FreqSeven = interData.FreqSeven;
                            tData.FreqEight = interData.FreqEight;
                            
                            tData.PressureDrop = RoundPD(roundData.TlossCoef.Value * Math.Pow(dCalcVelocity / 4005.0D, 2));
                            tData.Weight = UMCLib.Round(1.25D * interData.Weight, 0);
                            tData.Acceptable = 0;

                            secondInter.Add(tData);
                        }
                    }
                }

                for (int i = 0; i < secondInter.Count; i++)
                {
                    bool bBadPD = false;
                    bool bAlmostPD = false;
                    bool bAcceptablePD = false;
                    bool bBadAcoustics = false;
                    bool bAlmostAcoustics = false;
                    bool bAcceptableAcoustics = false;

                    SilencerResult interData = secondInter[i];

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

                        if (interData.FreqOne + 6.5D >= dOne && interData.FreqTwo + 3.5D >= dTwo && interData.FreqThree + 3.5D >= dThree && interData.FreqFour + 3.5D >= dFour &&
                            interData.FreqFive + 3.5D >= dFive && interData.FreqSix + 3.5D >= dSix && interData.FreqSeven + 3.5D >= dSeven && interData.FreqEight + 3.5D >= dEight)
                        {
                            bAlmostAcoustics = true;

                            if (interData.FreqOne >= dOne && interData.FreqTwo + .5D >= dTwo && interData.FreqThree + .5D >= dThree && interData.FreqFour + .5D >= dFour &&
                                interData.FreqFive + .5D >= dFive && interData.FreqSix + .5D >= dSix && interData.FreqSeven + .5D >= dSeven && interData.FreqEight + .5D >= dEight)
                            {
                                bAcceptableAcoustics = true;
                            }
                        }
                    }

                    if (dDiameter < 12 && (interData.Model.StartsWith("CSFMVL25") || interData.Model.StartsWith("CSFHVL20") || interData.Model.StartsWith("CSFHVL45") ||
                        interData.Model.StartsWith("CSFMVL45") ||interData.Model.StartsWith("CSFMVL55") ||interData.Model.StartsWith("CSFHVL30")))
                    {
                        interData.Acceptable = 0;
                    }
                    else if (bBadPD && bBadAcoustics)
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

                roundsilencer.Silencers = new List<SilencerResult>();

                for (int i = 0; i < secondInter.Count; i++)
                {
                    SilencerResult interData = secondInter[i];

                    if (!String.IsNullOrEmpty(roundsilencer.Type) && interData.Type != roundsilencer.Type)
                    {
                        // If filtering based on silencer type and not a match, skip it
                        continue;
                    }

                    switch (interData.Acceptable)
                    {
                        case 5:
                            roundsilencer.Silencers.Add(interData);
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
                            if (interData.PressureDrop > dPressureDrop * 1.1D)
                            {
                                interData.PressureDrop *= -1.0D;
                            }
                            roundsilencer.Silencers.Add(interData);
                            break;
                        default:
                            break;
                    }
                }

                roundsilencer.Silencers.Sort(RoundSort);
            }
            catch ( Exception ex )
            {
                return BadRequest(ex.Message + "InnerEx:" + ex.InnerException.Message);
            }
         
            return Ok(roundsilencer);
        }
    }
}
