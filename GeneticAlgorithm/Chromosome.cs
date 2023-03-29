using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm;


namespace GeneticAlgorithm
{

    public class Chromosome
    {
        private static Random random = new Random();

        public int[] genes;
        public int fitness;

        public Chromosome(int[] genes)
        {
            this.genes = genes;
            this.fitness = CalculateFitness();
        }

        public int CalculateFitness()
        {
            int totalWeight = 0;
            int totalValue = 0;
            int numFood = 0;
            int numClothing = 0;
            int numElectronics = 0;

            for (int i = 0; i < genes.Length; i++)
            {
                if (genes[i] == 1)
                {
                    totalWeight += GenAlgSettings.weights[i];
                    totalValue += GenAlgSettings.values[i];
                    if (GenAlgSettings.categories[i] == "food") numFood++;
                    if (GenAlgSettings.categories[i] == "clothing") numClothing++;
                    if (GenAlgSettings.categories[i] == "electronics") numElectronics++;
                }
            }

            if (totalWeight > GenAlgSettings.maxWeight) return 0;
            if (numFood < 3 || numClothing < 1 || numElectronics < 1) return 0;

            return totalValue;
        }

        public static Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            int[] childGenes = new int[parent1.genes.Length];

            int crossoverPoint = random.Next(parent1.genes.Length);
            for (int i = 0; i < crossoverPoint; i++)
            {
                childGenes[i] = parent1.genes[i];
            }
            for (int i = crossoverPoint; i < childGenes.Length; i++)
            {
                childGenes[i] = parent2.genes[i];
            }

            return new Chromosome(childGenes);
        }

        public void Mutate()
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (random.NextDouble() < GenAlgSettings.mutationRate)
                {
                    genes[i] = 1 - genes[i];
                }
            }

            fitness = CalculateFitness();
        }
    }
}
