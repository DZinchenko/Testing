using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPhase
{
    class PowerPhaseProfiler
    {
        private readonly double powerDiff;
        private readonly double timeDiff;
        public List<Phase> phases;

        private int phaseCacheInd = 0;
        private double powerSumCache = 0;
        private int readingNumCache = 0;
        private Phase CurrentPhase => phases.Last();

        public PowerPhaseProfiler(double powerDiff, double timeDiff)
        {
            this.powerDiff = powerDiff;
            this.timeDiff = timeDiff;
            this.phases = new List<Phase>();
        }

        public void Profile(double time, double power, bool isReprofiling = false)
        {
            if (phases.Count == 0)
            {
                phases.Add(new Phase
                {
                    StartTime = time,
                    PowerSum = power,
                    ReadingNum = 1
                });
                return;
            }

            if (Math.Abs(power - CurrentPhase.MidPower) > this.powerDiff)
            {
                CurrentPhase.EndTime = time;
                //if current phase was not logn enough - cache it's data and start profiling at
                //this point again - it can be a new phase as well as a continuation of the previous
                if (CurrentPhase.Duration < this.timeDiff && this.phases.Count > 1)
                {
                    var lastPhase = this.phases[this.phases.Count - 2];
                    lastPhase.EndTime = CurrentPhase.EndTime;
                    this.powerSumCache += CurrentPhase.PowerSum;
                    this.readingNumCache += CurrentPhase.ReadingNum;
                    this.phaseCacheInd = this.phases.Count - 2;

                    this.phases.Remove(CurrentPhase);
                    Profile(time, power, true);
                    return;
                }

                phases.Add(new Phase
                {
                    StartTime = time,
                    PowerSum = power,
                    ReadingNum = 1
                });
            }
            else
            {
                CurrentPhase.PowerSum += power;
                CurrentPhase.ReadingNum++;

                //if current phase is longer than minimum, try add cached phase data to a destination phase
                if (time - CurrentPhase.StartTime > timeDiff
                    && this.phases.Count > 1
                    && this.phases.Count - 2 == this.phaseCacheInd)
                {
                    var lastPhase = this.phases[this.phases.Count - 2];
                    lastPhase.PowerSum += powerSumCache;
                    lastPhase.ReadingNum += readingNumCache;
                    this.powerSumCache = 0;
                    this.readingNumCache = 0;
                }
            }
        }
    }
}
