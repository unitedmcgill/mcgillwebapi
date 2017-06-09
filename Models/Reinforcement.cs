using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Reinforcement
    {
        public string DuctType {get; set;}
        public string CalcType {get; set;}
        public string PressureClass {get; set;}
        public string Application {get; set;}
        public decimal Minor {get;set;}
        public decimal Major {get; set;}
        public int Gauge {get;set;}
        public int SelectedReinforcement {get;set;}
        public string MinorReinforcement {get;set;}
        public string MajorReinforcement {get;set;}
        public int CalculatedGauge {get;set;}
        public Reinforcement()
        {
        }
    }
}
