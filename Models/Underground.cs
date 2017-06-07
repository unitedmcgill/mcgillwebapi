using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Underground
    {
        public bool Spiral {get; set;}
        public string LoadType {get; set;}
        public decimal DistLoad {get; set;}
        public decimal Vehicle {get; set;}
        public decimal Area {get;set;}
        public string Material {get; set;}
        public decimal Diameter {get;set;}
        public int Gauge {get;set;}
        public decimal Density {get;set;}
        public decimal Depth {get;set;}
        public decimal Modulus {get;set;}
        public decimal SoilLoad {get;set;}
        public decimal ExternalLoad {get;set;}
        public decimal TotalLoad {get;set;}
        public decimal Deflection {get;set;}
        public decimal MaxDepth {get;set;}
        public string PassFail {get;set;}
        public Underground()
        {
        }
    }
}
