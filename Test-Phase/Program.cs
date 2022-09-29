using TestPhase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestPhase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input minimal phase power difference:");
            double diff = double.Parse(Console.ReadLine());
            Console.WriteLine("Input minimal phase power difference time interval:");
            double diffTime = double.Parse(Console.ReadLine());
            Console.WriteLine("Input time points (use ' ' as delimiter):");
            double[] time = Console.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();
            Console.WriteLine("Input power points (use ' ' as delimiter):");
            double[] power = Console.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();

            var profiler = new PowerPhaseProfiler(diff, diffTime);

            for (int i = 0; i < time.Length; i++)
            {
                profiler.Profile(time[i], power[i]);
            }

            Console.WriteLine("Phases:");
            for (int i = 0; i < profiler.phases.Count; i++)
            {
                Console.WriteLine($"Phase #{i}: length = {profiler.phases[i].Duration}, midpower = {profiler.phases[i].MidPower}");
            }
        }
    }
}