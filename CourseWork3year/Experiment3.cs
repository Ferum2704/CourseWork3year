using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class Experiment3
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

        List<double> tripleGreedyValues = new List<double>();
        List<double> avgGreedyValues = new List<double>();
        List<double> geneticValues = new List<double>();

        foreach (int size in matricesSizes)
        {
            double tripleObjectiveFunction = 0;
            double avarageObjectiveFunction = 0;
            double geneticObjectiveFunction = 0;

            for (int i = 0; i < RUNS; i++)
            {
                TimeMatrix matrix = new TimeMatrix(size);

                GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(8, size, 0.2, 5, 20);
                ObjectiveFunction.SetData(matrix.Data);

                matrix.FillWithRandomValues(meanValue, semiInterval);

                tripleObjectiveFunction += TripleGreedy.Execute(matrix.Data).Item1;
                avarageObjectiveFunction += TripleGreedy.Execute(matrix.Data).Item1;
                geneticObjectiveFunction += geneticAlgorithm.ExecuteObj(true);
            }

            tripleGreedyValues.Add(tripleObjectiveFunction / RUNS);
            avgGreedyValues.Add(avarageObjectiveFunction / RUNS);
            geneticValues.Add(geneticObjectiveFunction / RUNS);
        }

        ExcelWriter.OutputSizeToPrecisionComparison(tripleGreedyValues, avgGreedyValues, geneticValues, matricesSizes.ToList(), "Розмір/Точність");
    }

    private static void ExecuteWithoutRuns()
    {
        List<double> tripleGreedyValues = new List<double>();
        List<double> avgGreedyValues = new List<double>();
        List<double> geneticValues = new List<double>();

        foreach (var matrix in timeMatrices)
        {
            double tripleObjectiveFunction = 0;
            double avarageObjectiveFunction = 0;
            double geneticObjectiveFunction = 0;

            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(matrix.NumberOfPerformers, matrix.NumberOfPerformers, 0.2, 5, 20);
            ObjectiveFunction.SetData(matrix.Data);

            matrix.FillWithRandomValues(meanValue, semiInterval);

            tripleObjectiveFunction += TripleGreedy.Execute(matrix.Data).Item1;
            avarageObjectiveFunction += TripleGreedy.Execute(matrix.Data).Item1;
            geneticObjectiveFunction += geneticAlgorithm.ExecuteObj(true);
        }

        ExcelWriter.OutputSizeToPrecisionComparison(tripleGreedyValues, avgGreedyValues, geneticValues, matricesSizes.ToList(), "Розмір/Точність");
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
        MatrixHelper.WriteListOfMatricesToFile(matrices, "experiment3testData.txt");
    }
}
