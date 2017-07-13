using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class SilencerResult
    {
        public string Model {get; set;}
        public double? Length {get; set;}
        public double? FreqOne {get; set;}
        public double? FreqTwo {get; set;}
        public double? FreqThree {get; set;}
        public double? FreqFour {get; set;}
        public double? FreqFive {get; set;}
        public double? FreqSix {get; set;}
        public double? FreqSeven {get; set;}
        public double? FreqEight {get; set;}
        public double PressureDrop {get; set;}
        public double Weight {get; set;}
        public int Acceptable {get; set;}       
        public double Velocity {get; set;}
        public string Type {get; set;}
        public int Selected {get; set;}
        public int Diameter {get; set;}
        public SilencerResult()
        {

        }
    }
}
