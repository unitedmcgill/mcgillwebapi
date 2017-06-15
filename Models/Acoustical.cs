using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McGillWebAPI.Models
{
    public class Acoustical
    {
        public string CalcType {get; set;}
        public int Levels {get; set;}
        public decimal LengthBefore {get; set;}
        public decimal LengthAfter {get; set;}
        public decimal Distance {get;set;}
        public int[,] InputLevels {get; set;}
        public string OutputDesc {get; set;}
        public int[,] OutputLevels {get; set;}
        public decimal Overall {get; set;}
        public Acoustical()
        {
        }
    }
}
