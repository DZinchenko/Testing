using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPhase
{
    class Phase
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double PowerSum { get; set; }
        public int ReadingNum { get; set; }

        public double Duration => EndTime != 0 ? EndTime - StartTime : 0;
        public double MidPower => PowerSum / ReadingNum;
    }
}
