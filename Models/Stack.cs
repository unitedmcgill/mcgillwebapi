using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Stack
    {
        public decimal SafetyFactor {get;set;}
        public bool Spiral {get; set;}
        public string Material {get; set;}
        public decimal Diameter {get;set;}
        public decimal Wind {get;set;}
        public decimal Height {get;set;}
        public int Gauge {get;set;}
        public decimal Velocity {get; set;}
        public decimal Stress {get; set;}
        public decimal Buckling {get; set;}
        public decimal Yield {get; set;}
        public decimal Deflection {get; set;}
        public string PassFail {get;set;}
        public Stack()
        {
        }
    }
}
