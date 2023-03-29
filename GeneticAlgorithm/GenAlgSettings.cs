using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public static class GenAlgSettings
    {
        public static int[] weights;
        public static int[] values;
        public static string[] categories;
        public static int maxWeight = 100;
        public static int populationSize = 10;
        public static int numGenerations = 50;
        public static int chromosomeSize = 20;
        public static int tournamentSize = 5;
        public static double mutationRate = 0.05;

        public static void Init()
        {
            weights = new int[chromosomeSize];
            values = new int[chromosomeSize];
            categories = new string[chromosomeSize];

            Random random = new Random();

            for (int i = 0; i < chromosomeSize; i++)
            {
                weights[i] = random.Next(1, 11);
                values[i] = random.Next(1, 101);
                if (i % 3 == 0)
                {
                    categories[i] = "food";
                }
                else if (i % 3 == 1)
                {
                    categories[i] = "clothing";
                }
                else
                {
                    categories[i] = "electronics";
                }
            }
        }
        
    }
}
