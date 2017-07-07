using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class RoundSilencer
    {
        public string Model { get; set; }
        public int Diameter { get; set; }
        public int Length { get; set; }
        public int Velocity { get; set; }
        public int? Weight { get; set; }
        public int? FreqOne { get; set; }
        public int? FreqTwo { get; set; }
        public int? FreqThree { get; set; }
        public int? FreqFour { get; set; }
        public int? FreqFive { get; set; }
        public int? FreqSix { get; set; }
        public int? FreqSeven { get; set; }
        public int? FreqEight { get; set; }
        public string Type { get; set; }
    }
}
