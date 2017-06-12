using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Factair
    {
        public int Model {get; set;}
        public decimal Velocity {get; set;}
        public string Position {get; set;}
        public int Distance {get; set;}
        public decimal MaxVelocity {get;set;}
        public decimal PressureDrop {get; set;}
        public decimal[] Octaves {get; set;}
        public Factair()
        {
        }
    }
}
