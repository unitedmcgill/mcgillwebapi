using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class DDF
    {
        public string CalcType {get; set;}
        public int VelocityRange {get; set;}
        public decimal Diameter {get; set;}
        public int ConstDiameter {get; set;}
        public decimal CFM {get;set;}
        public decimal Length {get; set;}
        public List<string> DuctList { get; set; }
        public List<string> OrificeList { get; set;}
        public int EnteringVelocity {get;set;}
        public decimal TotalPressureDrop {get;set;}
        public string Error {get; set;}
        public DDF()
        {
        }
    }
}
