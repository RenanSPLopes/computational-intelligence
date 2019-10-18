using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Samples
{
    internal class SteadyStateModel
    {
        private const int NumberOfProcesses = 25;
        private const int PopSize = 100;
        private const int NumberOfGenerations = 10000;
        private static readonly int[] _capacity = { 1000, 800, 700 };

        public static void Generate()
        {
            var random = new Random();

            Console.WriteLine("*** Exemplo primeiro modelo Computação Evolutiva - Escalonamento de processos***");

            var numberOfCores = _capacity.Length;
            var population = new int[PopSize][];
            var csvBuilder = new StringBuilder();

            Helper.GeneratePopulation(PopSize, NumberOfProcesses, random, numberOfCores, population);

            for (var generationId = 0; generationId < NumberOfGenerations; generationId++)
            {
                var parentId = random.Next(0, PopSize);

                var son = (int[])population[parentId].Clone();
                Mutate(random, son);

                var selectedId = random.Next(0, PopSize);

                var fitnessClone = Fitness(son, _capacity);
                var fitnessSelected = Fitness(population[selectedId], _capacity);

                if (fitnessClone > fitnessSelected)
                    population[selectedId] = son;

                var fit = new double[PopSize];
                for (var i = 0; i < PopSize; i++)
                {
                    var subject = population[i];
                    fit[i] = Fitness(subject, _capacity);
                }

                var line = $"{generationId};{fit.Average()};{Helper.CalculateStandardDeviation(fit)}";
                csvBuilder.AppendLine(line);
            }

            File.WriteAllText("output.csv", csvBuilder.ToString());
        }

        private static void Mutate(Random random, IList<int> clone)
        {
            var id = random.Next(0, NumberOfProcesses);
            clone[id] = random.Next(0, _capacity.Length);
        }

        private static double Fitness(IEnumerable<int> subject, IReadOnlyList<int> capacity)
        {
            const double cost = 1000;

            var v = new double[capacity.Count];

            foreach (var coreId in subject)
            {
                v[coreId] = v[coreId] + cost / capacity[coreId];
            }

            return 1 / v.Max();
        }
    }
}