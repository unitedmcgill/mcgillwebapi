using System;
using System.Collections.Generic;

namespace McGillWebAPI.Model
{
    public partial class RoundSilencerExtraData
    {
        public string Model { get; set; }
        public double? NormLossCoef { get; set; }
        public double? TlossCoef { get; set; }
        public int? NumberSizes { get; set; }
        public int? StaticLenDia { get; set; }
    }
}
