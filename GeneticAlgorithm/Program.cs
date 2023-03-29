using GeneticAlgorithm;
using System;

class Program
{
    public static Random random = new Random();

    static void Main(string[] args)
    {
        GenAlgSettings.Init();


        List<Chromosome> population = new List<Chromosome>();
        for (int i = 0; i < GenAlgSettings.populationSize; i++)
        {
            int[] genes = new int[GenAlgSettings.chromosomeSize];
            for (int j = 0; j < genes.Length; j++)
            {
                genes[j] = random.Next(2);
            }
            population.Add(new Chromosome(genes));
        }

        // Evolve population
        for (int generation = 0; generation < GenAlgSettings.numGenerations; generation++)
        {
            population.Sort((c1, c2) => c2.fitness.CompareTo(c1.fitness));

            PrintPopulationInfo(population, generation);

            List<Chromosome> childPopulation = new List<Chromosome>();
            while (childPopulation.Count < GenAlgSettings.populationSize)
            {
                Chromosome parent1 = TournamentSelection(population);
                Chromosome parent2 = TournamentSelection(population);
                Chromosome child = Chromosome.Crossover(parent1, parent2);
                //child.Mutate();
                childPopulation.Add(child);
            }

            List<Chromosome> newPopulation = population;
            foreach (Chromosome child in childPopulation)
            {
                newPopulation.Add(child);
            }
            newPopulation.Sort((c1, c2) => c2.fitness.CompareTo(c1.fitness));

            newPopulation.RemoveRange(GenAlgSettings.populationSize - 1, GenAlgSettings.populationSize);

            foreach(Chromosome chromosome in newPopulation)
            {
                chromosome.Mutate();
            }

            population = newPopulation;
        }

        // Print final best solution
        population.Sort((c1, c2) => c2.fitness.CompareTo(c1.fitness));
        Console.WriteLine($"Final best solution = {population[0].fitness}");
        Console.ReadLine();
    }

    static Chromosome TournamentSelection(List<Chromosome> population)
    {
        List<Chromosome> tournament = new List<Chromosome>();
        for (int i = 0; i < GenAlgSettings.tournamentSize; i++)
        {
            tournament.Add(population[random.Next(GenAlgSettings.populationSize)]);
        }
        tournament.Sort((c1, c2) => c2.fitness.CompareTo(c1.fitness));
        return tournament[0];
    }

    public static void PrintPopulationInfo(List<Chromosome> population, int populationNum)
    {
        Console.WriteLine("\n------------------------------------------------");

        Console.WriteLine($"Population number: {populationNum}\n");

        foreach (Chromosome chromosome in population)
        {
            PrintGens(chromosome);
        }

    }

    public static void PrintGens(Chromosome chromosome)
    {
        for (int i = 0; i < GenAlgSettings.chromosomeSize; i++)
        {
            Console.Write(chromosome.genes[i] + " ");
        }

        Console.Write($"fitness:{chromosome.fitness}\n");
    }
}