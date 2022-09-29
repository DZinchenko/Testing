using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_ApModel
{
    class Aperiodic
    {
        private readonly double dt;
        private readonly double K;
        private readonly double T;
        private double prev;

        public Aperiodic(double dt, double K, double T)
        {
            this.dt = dt;
            this.K = K;
            this.T = T;
        }

        public double Calc(double x)
        {
            var y = (this.K * this.dt * x + this.T * this.prev) / (this.T + this.dt);
            this.prev = y;
            return y;
        }
    }
}
