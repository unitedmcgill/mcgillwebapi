using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class OrificeTube
    {
        public decimal TestPressure {get; set;}
        public decimal CFM {get; set;}
        public string Plate {get; set;}
        public decimal TubeDiameter {get; set;}
        public decimal OrificeDiameter {get;set;}
        public decimal BetaRatio {get; set;}
        public string OpenArea {get;set;}
        public decimal[,] TubeList {get;set;}
        public OrificeTube()
        {
        }
    }
}
