using System;
using System.Linq;

namespace Exemplos
{
    class Program
    {

       static double Fitness(int[] clone, int[] capacity){
           const int cost = 1000;

            var v = new double[capacity.Length];

            for(int i = 0; i < v.Length; i++)
                v[i] = 0;

            for(int i = 0; i < clone.Length; i++){
                var coredId = clone[i];
                v[coredId] = v[coredId] + cost/capacity[coredId];
            }

            return 1/ v.Max();
        }

        static void Main(string[] args)
        {
            // (capacity, nprocess=10, popsize=100, ngenerations=1000)
            const int nprocess = 25;
            const int popSize = 100;
            const int ngenerations = 1000;
            int[] capacity = {1000, 800, 700 };
            var random = new Random();

            Console.WriteLine("*** Exemplo primeiro modelo Computação Evolutiva - Escalonamento de processos***");

            // Gera população
            var ncores = capacity.Length;
            int[][] population = new int[popSize][];
    
            for(int i = 0; i < popSize; i++){
                var aux = new int[nprocess];

                 for(int j=0; j < nprocess; j++)
                    aux[j] = random.Next(0, ncores);  

                population[i] = aux; 
            }
                
            for(int generationId = 0; generationId < ngenerations; generationId++){
                    var parentId  = random.Next(0, popSize);

                    // Clonagem
                    var clone = population[parentId];

                    //Mutação
                    var id = random.Next(0, nprocess);
                    clone[id] = random.Next(0, ncores);

                    // Selecionando um elemento aleatório para competição
                    var selectedId = random.Next(0, popSize);

                    var fitness_clone = Fitness(clone, capacity);
                    var fitness_selected = Fitness(population[selectedId], capacity);

                    if(fitness_clone > fitness_selected)
                        population[selectedId] = clone;
                    
                            
                    var fit = new double[nprocess];
                    for(int i = 0; i < nprocess; i++){
                        fit[i] = Fitness(population[i], capacity);
                    }

                    Console.WriteLine($"{generationId} - {fit.Average().ToString("0.####")} {sd(fit).ToString("0.####")}");

            }
        }

        static double sd(double[] v){
             //Compute the Average      
            double avg = v.Average();
            //Perform the Sum of (value-avg)_2_2      
            double sum = v.Sum(d => Math.Pow(d - avg, 2));
            //Put it all together      
            return Math.Sqrt((sum) / (v.Count()-1)); 
        }
    }
}
