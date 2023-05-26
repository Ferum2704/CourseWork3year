using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public class GeneticAlgorithm
{
    private readonly int _numberOfInitialChromosomos;
    private readonly int _numberOfGenes;
    private readonly double _mutationProbability;
    private readonly double _maxNumberOfConsecutiveSameObjFuncValue;
    private readonly int _numberOfIterations;

    public GeneticAlgorithm(
        int numberOfInitialChromosomos, 
        int numberOfGenes, 
        double mutationProbability, 
        int maxNumberOfConsecutiveSameObjFuncValue, 
        int numberOfIterations)
    {
        _numberOfInitialChromosomos = numberOfInitialChromosomos;
        _numberOfGenes = numberOfGenes;
        _mutationProbability = mutationProbability;
        _maxNumberOfConsecutiveSameObjFuncValue = maxNumberOfConsecutiveSameObjFuncValue;
        _numberOfIterations = numberOfIterations;
    }

    public (int objFuncValue, int[] distribution) Execute(bool useIterationsExitApproach)
    {
        var population = GenerateInitialChromosomos();
        List<int> chromosomesObjFuncValues = new();
        int theBestObjFuncValue = 0;
        int[] theBestChromosome = new int[_numberOfGenes];

        if (useIterationsExitApproach)
        {
            for (int i = 0; i < _numberOfIterations; i++)
            {
                ExecuteMainPart(ref population, ref chromosomesObjFuncValues);
            }

            theBestObjFuncValue = chromosomesObjFuncValues.Min();
            var theBestChromosomeIndex = chromosomesObjFuncValues.FindIndex(x => x == theBestObjFuncValue);
            theBestChromosome = population[theBestChromosomeIndex];
        }
        else
        {
            int previousTheBestObjFuncValue = 0;
            int numberOfConsecutiveSameObjFuncValue = 0;
            while (numberOfConsecutiveSameObjFuncValue <= _maxNumberOfConsecutiveSameObjFuncValue)
            {
                ExecuteMainPart(ref population, ref chromosomesObjFuncValues);

                theBestObjFuncValue = chromosomesObjFuncValues.Min();

                if (theBestObjFuncValue == previousTheBestObjFuncValue)
                {
                    numberOfConsecutiveSameObjFuncValue++;
                }
                else
                {
                    numberOfConsecutiveSameObjFuncValue = 0;
                }

                previousTheBestObjFuncValue = theBestObjFuncValue;
                var theBestChromosomeIndex = chromosomesObjFuncValues.FindIndex(x => x == theBestObjFuncValue);
                theBestChromosome = population[theBestChromosomeIndex];
            }
        }

        return (theBestObjFuncValue, theBestChromosome);
    }

    private void ExecuteMainPart(ref List<int[]> population, ref List<int> chromosomesObjFuncValues)
    {
        var rand = new Random();

        chromosomesObjFuncValues = CalculateObjFuncValues(population);
        var parents = ChooseParents(population, chromosomesObjFuncValues);
        var childen = CrossParents(parents[0], parents[1]);

        if (rand.NextDouble() < _mutationProbability)
        {
            var childenToMutate = childen[rand.Next(0, 2)];
            MutateChild(childenToMutate);
        }

        foreach (var child in childen)
        {
            ObjectiveFunction.SetDistribution(child);
            chromosomesObjFuncValues.Add(ObjectiveFunction.Calculate());
        }

        population = GetNewPopulation(population.Concat(childen).ToList(), chromosomesObjFuncValues);
    }

    private List<int[]> GenerateInitialChromosomos()
    {
        List<int[]> arrays = new List<int[]>();

        for (int i = 0; i < _numberOfInitialChromosomos; i++)
        {
            int[] array = Enumerable.Range(1, _numberOfGenes).OrderBy(x => Guid.NewGuid()).ToArray();
            arrays.Add(array);
        }

        return arrays;
    }

    private List<int[]> ChooseParents(List<int[]> chromosomePopulation, List<int> chromosomesObjFuncValues)
    {
        List<int[]> parents = new(2);

        parents.Add(chromosomePopulation[GetChromosomeWithMaxObjFunctionValue(chromosomesObjFuncValues.ToArray())]);
        parents.Add(chromosomePopulation[GetChromosomeWithMinObjFunctionValue(chromosomesObjFuncValues.ToArray())]);

        return parents;
    }

    private List<int> CalculateObjFuncValues(List<int[]> chromosomePopulation)
    {
        List<int> chromosomesObjFuncValues = new();

        foreach (var chromosome in chromosomePopulation)
        {
            ObjectiveFunction.SetDistribution(chromosome);
            chromosomesObjFuncValues.Add(ObjectiveFunction.Calculate());
        }

        return chromosomesObjFuncValues;
    }

    private int GetChromosomeWithMaxObjFunctionValue(int[] objFuncValues)
    {
        int maxValue = 0;
        int chromosome = 0;
        for (int i = 0; i < objFuncValues.Length; i++)
        {
            if (objFuncValues[i] > maxValue)
            {
                maxValue = objFuncValues[i];
                chromosome = i;
            }
        }
        return chromosome;
    }

    private int GetChromosomeWithMinObjFunctionValue(int[] objFuncValues)
    {
        int minValue = int.MaxValue;
        int chromosome = 0;
        for (int i = 0; i < objFuncValues.Length; i++)
        {
            if (objFuncValues[i] < minValue)
            {
                minValue = objFuncValues[i];
                chromosome = i;
            }
        }
        return chromosome;
    }

    private List<int[]> CrossParents(int[] parent1, int[] parent2)
    {
        List<int[]> childen = new(2);
        childen.Add(OrderCrossover(parent1, parent2));
        childen.Add(OrderCrossover(parent2, parent1));

        return childen;
    }

    private int[] OrderCrossover(int[] baseParent, int[] secondaryParent)
    {
        int n = baseParent.Length;
        int[] child = new int[n];

        Random random = new();
        int k1 = random.Next(0, n-1);
        int k2 = random.Next(k1 + 1, n);

        Array.Copy(baseParent, k1, child, k1, k2 - k1 + 1);

        List<int> ySequence = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (!child.Contains(secondaryParent[i]))
            {
                ySequence.Add(secondaryParent[i]);
            }
        }

        int j = k2+1;
        while (ySequence.Count != 0)
        {
            if (j == n)
            {
                j = 0;
            }
            if ((j > k2 && j < n) || (j >= 0 && j < k1))
            {
                child[j] = ySequence[0];
                ySequence.RemoveAt(0);
                j++;
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (child[i] == 0)
            {
                int missingGene = secondaryParent.FirstOrDefault(gene => !child.Contains(gene));
                child[i] = missingGene;
            }
        }

        return child;
    }

    public void MutateChild(int[] child)
    {
        var rand = new Random();

        int sourseMutationIndex = rand.Next(0, child.Length);
        int targetMutationIndex = rand.Next(0, child.Length);

        int elementToMutate = child[sourseMutationIndex];

        if (targetMutationIndex < sourseMutationIndex)
        {
            for (int i = sourseMutationIndex; i > targetMutationIndex; i--)
            {
                child[i] = child[i - 1];
            }
        }
        else if(targetMutationIndex > sourseMutationIndex)
        {
            var lastElement = child[child.Length - 1];
            for (int i = child.Length-1; i > targetMutationIndex; i--)
            {
                child[i] = child[i - 1];
            }

            for (int i = sourseMutationIndex; i > 0; i--)
            {
                child[i] = child[i - 1];
            }

            child[0] = lastElement;
        }

        child[targetMutationIndex] = elementToMutate;
    }

    private List<int[]> GetNewPopulation(List<int[]> currentPopulationAndChildren, List<int> chromosomesObjFuncValues)
    {
        var smallestChromosomesIndexes = chromosomesObjFuncValues.Select((value, index) => new { Value = value, Index = index })
                         .OrderByDescending(item => item.Value)
                         .Take(2)
                         .Select(item => item.Index)
                         .ToList();
        var firstIndexToDelete = smallestChromosomesIndexes[0];
        var secondIndexToDelete = smallestChromosomesIndexes[1];
        if (firstIndexToDelete < secondIndexToDelete)
        {
            currentPopulationAndChildren.RemoveAt(firstIndexToDelete);
            chromosomesObjFuncValues.RemoveAt(firstIndexToDelete);
            currentPopulationAndChildren.RemoveAt(secondIndexToDelete-1);
            chromosomesObjFuncValues.RemoveAt(secondIndexToDelete - 1);
        }
        else
        {
            currentPopulationAndChildren.RemoveAt(secondIndexToDelete);
            chromosomesObjFuncValues.RemoveAt(secondIndexToDelete);
            currentPopulationAndChildren.RemoveAt(firstIndexToDelete - 1);
            chromosomesObjFuncValues.RemoveAt(firstIndexToDelete - 1);
        }



        return currentPopulationAndChildren;
    }

    public void PrintArray(int[] array)
    {
        foreach (var element in array)
        {
            Console.Write(element + " ");
        }
        Console.WriteLine();
    }
}
