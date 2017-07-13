using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class RectSilencerInput
    {
        public int CFM {get; set;}
        public decimal Width {get; set;}
        public decimal Height {get; set;}
        public decimal PressureDrop {get; set;}
        public decimal CalcVelocity {get;set;}
        public decimal DisplayVelocity {get;set;}
        public int Freq1 {get; set;}
        public int Freq2 {get; set;}
        public int Freq3 {get; set;}
        public int Freq4 {get; set;}
        public int Freq5 {get; set;}
        public int Freq6 {get; set;}
        public int Freq7 {get; set;}
        public int Freq8 {get; set;}
        public int Elbow {get; set;}
        public int Louver {get; set;}
        public int Wide {get; set;}
        public string Type {get; set;}
        public List<SilencerResult> Silencers { get; set;}
        public RectSilencerInput()
        {
        }
    }
}
