using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public enum DuctType  {Round, Rectangular, Oval};
    public class DuctConvert
    {
        public string Name { get; set; }
        public DuctType Type {get; set; }
        public decimal RectMinor {get; set;}
        public decimal RectMajor {get; set;}
        public decimal OvalMinor {get;set;}
        public decimal OvalMajor {get; set;}
        public decimal Diameter {get; set;}
        public decimal Minor {get;set;}
        public decimal Result1 {get;set;}
        public decimal Result2 {get;set;}
        public DuctConvert()
        {
        }
    }
}
