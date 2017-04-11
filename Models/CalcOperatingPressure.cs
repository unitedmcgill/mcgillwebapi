using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class CalcOperatingPressure
    {
        public int Type {get; set;}
        public string Material {get; set;}
        public int Gauge {get; set;}
        public bool Spiral {get;set;}
        public decimal Diameter {get; set;}
        public decimal StiffenerSpacing {get; set;}
        public decimal DuctTemp {get;set;}
        public string DuctClass {get;set;}
        public decimal Pressure {get; set;}
        public decimal OperatingPressure {get;set;}
        public string StiffenerSize {get;set;}
        public CalcOperatingPressure()
        {
        }
    }
}
