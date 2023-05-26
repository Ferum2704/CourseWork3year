using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class Experiment2
{
    private static List<TimeMatrix> timeMatrices = new();

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
        Console.Write("Введіть перелік значень розмірностей квадратних матриць розділених пробілами:");
        int[] dimensions = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        Console.Write("Введіть значення математичного сподівання для елементів матриці:");
        int meanValue = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Введіть значення напівінтервалу для генерації елементів матриці:");
        int semiInterval = int.Parse(Console.ReadLine() ?? "0");

        foreach (var dimension in dimensions)
        {
            TimeMatrix matrix = new TimeMatrix(dimension);
            matrix.FillWithRandomValues(meanValue, semiInterval);
            timeMatrices.Add(matrix);
        }
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
