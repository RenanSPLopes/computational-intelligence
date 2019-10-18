using System;
using System.Linq;

namespace Samples
{
    internal class Helper
    {
        public static void GeneratePopulation(int popSize, int nprocess, Random random, int ncores, int[][] population)
        {
            for (var i = 0; i < popSize; i++)
            {
                var aux = new int[nprocess];

                for (var j = 0; j < nprocess; j++)
                    aux[j] = random.Next(0, ncores);

                population[i] = aux;
            }
        }

        public static double CalculateStandardDeviation(double[] v)
        {
            //Compute the Average
            var avg = v.Average();
            //Perform the Sum of (value-avg)_2_2
            var sum = v.Sum(d => Math.Pow(d - avg, 2));
            //Put it all together
            return Math.Sqrt((sum) / (v.Count() - 1));
        }
    }
}