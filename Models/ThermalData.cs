using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class ThermalData
    {
        public decimal Insulation {get; set;}
        public int Wind {get; set;}
        public decimal Diameter {get; set;}
        public decimal FlowRate {get; set;}
        public decimal InsideDuctTemp {get;set;}
        public decimal AmbientTemp {get; set;}
        public decimal DuctLength {get;set;}
        public decimal Humidity {get;set;}
        public decimal HeatTransfer {get;set;}
        public decimal SkinTemp {get;set;}
        public decimal ExitTemp {get;set;}
        public decimal DewpointTemp {get;set;}
        public string Condensation {get;set;}
        public decimal Conductivity {get;set;}
        public decimal Density {get;set;}
        public ThermalData()
        {
        }
    }
}
