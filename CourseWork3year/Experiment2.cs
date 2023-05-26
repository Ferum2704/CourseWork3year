using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class Experiment2
{
    private static List<TimeMatrix> timeMatrices = new();
    private static int[] matricesSizes;
    private static int meanValue;
    private static int semiInterval;
    private static bool isGenerated = false;

    public static void ReadFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines($"../../../{fileName}");

        // Індекс для відстеження поточної матриці
        int currentIndex = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Якщо рядок є порожнім, це означає нову матрицю
            if (string.IsNullOrEmpty(line))
            {
                currentIndex++; // Збільшення індексу для нової матриці
            }
            else
            {
                string[] elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                TimeMatrix matrix = new TimeMatrix(elements.Length);
                for (int j = 0; j < elements.Length; j++)
                {
                    matrix[currentIndex, j] = int.Parse(elements[j]);
                }

                // Додавання матриці до списку
                if (currentIndex >= timeMatrices.Count)
                {
                    timeMatrices.Add(matrix);
                }
                else
                {
                    timeMatrices[currentIndex] = matrix;
                }
            }
        }
    }

    public static void InputManually()
    {
        bool continueInput = true;
        while (continueInput)
        {
            int matrixSize;

            Console.Write("Введіть розмір квадратної матриці: ");
            while (!int.TryParse(Console.ReadLine(), out matrixSize) || matrixSize <= 0)
            {
                Console.WriteLine("Некоректний ввід. Будь ласка, введіть цілочислельне додатнє число:");
            }


            TimeMatrix matrix = new TimeMatrix(matrixSize);
            Console.WriteLine();

            for (int i = 0; i < matrixSize; i++)
            {
                Console.WriteLine($"Введіть елементи {i + 1}-го рядка, розділені пробілом:");
                string matrixRow = Console.ReadLine();
                string[] elements = matrixRow.Split(' ');

                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = int.Parse(elements[j]);
                }
            }

            timeMatrices.Add(matrix);

            Console.WriteLine("Матриця введена успішно");

            Console.WriteLine("Чи ви хочете ввести ще одну матрицю (Y/N)");
            string input = Console.ReadLine();
            continueInput = input.Equals("Y", StringComparison.OrdinalIgnoreCase);
            Console.WriteLine();
        }
    }

    public static void Generate()
    {
        isGenerated = true;
        Console.Write("Введіть перелік значень розмірностей квадратних матриць розділених пробілами:");
        matricesSizes = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        Console.Write("Введіть значення математичного сподівання для елементів матриці:");
        meanValue = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Введіть значення напівінтервалу для генерації елементів матриці:");
        semiInterval = int.Parse(Console.ReadLine() ?? "0");
    }

    public static void Execute()
    {
        if (isGenerated)
        {
            ExecuteWithRuns();
        }
        else
        {
            ExecuteWithoutRuns();
        }
        ExcelWriter.SaveToFile();
    }

    private static void ExecuteWithRuns()
    {
        const int RUNS = 20;

        Stopwatch stopWatchTripleAlgorithm = new Stopwatch();
        Stopwatch stopWatchAvarageAlgorithm = new Stopwatch();
        Stopwatch stopWatchGeneticAlgorithm = new Stopwatch();

        List<long> intervalsTripleGreedy = new List<long>();
        List<long> intervalsAvarageGreedy = new List<long>();
        List<long> intervalsGeneticAlgorithm = new List<long>();

        int tripleObjectiveFunction = 0;
        int avarageObjectiveFunction = 0;
        int geneticObjectiveFunction = 0;

        foreach (int size in matricesSizes)
        {
            for (int i = 0; i < RUNS; i++)
            {
                TimeMatrix matrix = new TimeMatrix(size);
                matrix.FillWithRandomValues(meanValue, semiInterval);

                stopWatchTripleAlgorithm.Start();
                (tripleObjectiveFunction, _) = TripleGreedy.Execute(matrix.Data);
                stopWatchTripleAlgorithm.Stop();

                stopWatchAvarageAlgorithm.Start();
                (avarageObjectiveFunction, _) = TripleGreedy.GetAvarageObjectiveFunction(matrix.Data);
                stopWatchAvarageAlgorithm.Stop();

                GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(8, size, 0.2, 5, 20);

                ObjectiveFunction.SetData(matrix.Data);

                stopWatchGeneticAlgorithm.Start();
                geneticObjectiveFunction = geneticAlgorithm.ExecuteObj(true);
                stopWatchGeneticAlgorithm.Stop();

            }

            intervalsTripleGreedy.Add(stopWatchTripleAlgorithm.ElapsedMilliseconds / RUNS);
            intervalsAvarageGreedy.Add(stopWatchAvarageAlgorithm.ElapsedMilliseconds / RUNS);
            intervalsGeneticAlgorithm.Add(stopWatchGeneticAlgorithm.ElapsedMilliseconds / RUNS);

            stopWatchTripleAlgorithm.Reset();
            stopWatchAvarageAlgorithm.Reset();
            stopWatchGeneticAlgorithm.Reset();
        }

        ExcelWriter.OutputSizeToTimeComparison(intervalsTripleGreedy, intervalsAvarageGreedy, intervalsGeneticAlgorithm, matricesSizes.ToList(), "Розмір/Час");
    }

    private static void ExecuteWithoutRuns()
    {
        Stopwatch stopWatchTripleAlgorithm = new Stopwatch();
        Stopwatch stopWatchAvarageAlgorithm = new Stopwatch();
        Stopwatch stopWatchGeneticAlgorithm = new Stopwatch();

        List<long> intervalsTripleGreedy = new List<long>();
        List<long> intervalsAvarageGreedy = new List<long>();
        List<long> intervalsGeneticAlgorithm = new List<long>();

        int tripleObjectiveFunction = 0;
        int avarageObjectiveFunction = 0;
        int geneticObjectiveFunction = 0;

        foreach (var matrix in timeMatrices)
        {
            stopWatchTripleAlgorithm.Start();
            (tripleObjectiveFunction, _) = TripleGreedy.Execute(matrix.Data);
            stopWatchTripleAlgorithm.Stop();

            stopWatchAvarageAlgorithm.Start();
            (avarageObjectiveFunction, _) = TripleGreedy.GetAvarageObjectiveFunction(matrix.Data);
            stopWatchAvarageAlgorithm.Stop();

            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(matrix.NumberOfPerformers, matrix.NumberOfPerformers, 0.2, 5, 20);

            ObjectiveFunction.SetData(matrix.Data);

            stopWatchGeneticAlgorithm.Start();
            geneticObjectiveFunction = geneticAlgorithm.ExecuteObj(true);
            stopWatchGeneticAlgorithm.Stop();

            intervalsTripleGreedy.Add(stopWatchTripleAlgorithm.ElapsedMilliseconds);
            intervalsAvarageGreedy.Add(stopWatchAvarageAlgorithm.ElapsedMilliseconds);
            intervalsGeneticAlgorithm.Add(stopWatchGeneticAlgorithm.ElapsedMilliseconds);

            stopWatchTripleAlgorithm.Reset();
            stopWatchAvarageAlgorithm.Reset();
            stopWatchGeneticAlgorithm.Reset();
        }

        ExcelWriter.OutputSizeToTimeComparison(intervalsTripleGreedy, intervalsAvarageGreedy, intervalsGeneticAlgorithm, matricesSizes.ToList(), "Розмір/Час");
    }

    public static void FillFileWithData()
    {
        List<TimeMatrix> matrices = new List<TimeMatrix>();
        TimeMatrix matrix1 = new TimeMatrix(10);
        TimeMatrix matrix2 = new TimeMatrix(100);
        TimeMatrix matrix3 = new TimeMatrix(1000);
        matrix1.FillWithRandomValues(100, 80);
        matrix2.FillWithRandomValues(100, 80);
        matrix3.FillWithRandomValues(100, 80);
        matrices.Add(matrix1);
        matrices.Add(matrix2);
        matrices.Add(matrix3);
        MatrixHelper.WriteListOfMatricesToFile(matrices, "experiment2testData.txt");
    }
}
