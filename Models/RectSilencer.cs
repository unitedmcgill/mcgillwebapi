using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class RectResult
    {
        public string Model {get; set;}
        public double? Length {get; set;}
        public double? FreqOne {get; set;}
        public double? FreqTwo {get; set;}
        public double? FreqThree {get; set;}
        public double? FreqFour {get; set;}
        public double? FreqFive {get; set;}
        public double? FreqSix {get; set;}
        public double? FreqSeven {get; set;}
        public double? FreqEight {get; set;}
        public double PressureDrop {get; set;}
        public double Weight {get; set;}
        public int Acceptable {get; set;}       
        public double Velocity {get; set;}
        public string Type {get; set;}

        public RectResult()
        {

        }
        // public RectResult( string model, double length, double f1, double f2, double f3, double f4, double f5, double f6, double f7, double f8,
        //                    double pdrop, double wt, int acceptable, double velocity, string type )
        // {
        //     this.Model = model;
        //     this.Length = length;
        //     this.FreqOne = f1;
        //     this.FreqTwo = f2;
        //     this.FreqThree = f3;
        //     this.FreqFour = f4;
        //     this.FreqFive = f5;
        //     this.FreqSix = f6;
        //     this.FreqSeven = f7;
        //     this.FreqEight = f8;    
        //     this.PressureDrop = pdrop;
        //     this.Weight = wt;
        //     this.Acceptable = acceptable;
        //     this.Velocity = velocity;
        //     this.Type = type;
        // }
    }

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
        public List<RectResult> Silencers { get; set;}
        public RectSilencerInput()
        {
        }
    }
}
