using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Offset
    {
        public string CalcType {get; set;}
        public string ConnectionType {get; set;}
        public decimal Diameter {get; set;}
        public decimal Distance {get; set;}
        public decimal Length {get;set;}
        public decimal CenterLine1 {get; set;}
        public decimal CenterLine2 {get; set;}
        public decimal Angle {get; set;}
        public string CalcLengthDesc {get; set;}
        public string CalcLength {get; set;}
        public Offset()
        {
        }
    }
}
