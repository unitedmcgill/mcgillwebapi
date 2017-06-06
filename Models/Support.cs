using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Support
    {
        public decimal Insulation {get; set;}
        public decimal Load {get; set;}
        public decimal Density {get; set;}
        public decimal SafetyFactor {get;set;}
        public bool Spiral {get; set;}
        public string Material {get; set;}
        public decimal Diameter {get;set;}
        public decimal Wind {get;set;}
        public decimal Snow {get; set;}
        public decimal RingSpacing {get;set;}
        public int InnerGauge {get;set;}
        public int OuterGauge {get;set;}
        public decimal AllowedDeflection {get;set;}
        public decimal ActualDeflection {get;set;}
        public decimal MaxLength {get;set;}
        public string PassFail {get;set;}
        public decimal MaterialLoad {get;set;}
        public Support()
        {
        }
    }
}
