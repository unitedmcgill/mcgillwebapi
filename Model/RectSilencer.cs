using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class RectSilencer
    {
        public string Model { get; set; }
        public double Length { get; set; }
        public int Velocity { get; set; }
        public double? WeightFactor { get; set; }
        public double? LossCoef { get; set; }
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
