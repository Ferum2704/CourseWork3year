using CourseWork3year.Experiments;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace CourseWork3year.Individual;

public static class IndividualTaskMode
{
    private static TimeMatrix individualMatrix;

    public static void Start()
    {
        Console.WriteLine("Виберіть спосіб задання індивідуальної задачі(матриці):");
        Console.WriteLine("1. Зчитати з файлу");
        Console.WriteLine("2. Ввести вручну");
        Console.WriteLine("3. Генерувати випадковим чином");
        Console.Write("Ваш вибір: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Введіть назву файлу: ");
                string fileName = Console.ReadLine();
                ReadFromFile(fileName);
                break;
            case 2:
                InputManually();
                break;
            case 3:
                Generate();
                break;
            default:
                Console.WriteLine("Некоректний вибір. Перезапустіть програму або перейдість до наступного експерименту");
                break;
        }

        if (individualMatrix != null)
        {
            Solve();
        }
    }

    private static void ReadFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines($"../../../Individual/Data/{fileName}");

        individualMatrix = new TimeMatrix(lines.Length);

        for (int i = 0; i < individualMatrix.NumberOfTasks; i++)
        {
            string[] values = lines[i].Split(' ');
            for (int j = 0; j < individualMatrix.NumberOfPerformers; j++)
            {
                int value = int.Parse(values[j]);
                individualMatrix[i, j] = value;
            }
        }

        Console.WriteLine("\nЗчитана матриця");
        individualMatrix.Print();
    }

    private static void InputManually()
    {
        int matrixSize;

        Console.Write("Введіть розмір квадратної матриці: ");
        while (!int.TryParse(Console.ReadLine(), out matrixSize) || matrixSize <= 0)
        {
            Console.WriteLine("Некоректний ввід. Будь ласка, введіть цілочислельне додатнє число:");
        }

        individualMatrix = new TimeMatrix(matrixSize);

        for (int i = 0; i < matrixSize; i++)
        {
            Console.WriteLine($"Введіть елементи {i + 1}-го рядка, розділені пробілом:");
            string matrixRow = Console.ReadLine();
            string[] elements = matrixRow.Split(' ');

            for (int j = 0; j < matrixSize; j++)
            {
                individualMatrix[i, j] = int.Parse(elements[j]);
            }
        }
    }

    private static void Generate()
    {
        int matrixSize;
        int meanValue;
        int semiInterval;
        Console.Write("Введіть розмірність квадратної матриці: ");
        matrixSize = int.Parse(Console.ReadLine());
        Console.Write("Введіть значення математичного сподівання: ");
        meanValue = int.Parse(Console.ReadLine());
        Console.Write("Введіть значення напівінтервалу: ");
        semiInterval = int.Parse(Console.ReadLine());

        individualMatrix = new TimeMatrix(matrixSize);
        individualMatrix.FillWithRandomValues(meanValue, semiInterval);

        Console.WriteLine("\nЗгенерована матриця: ");
/*        individualMatrix.Print();*/
    }

    private static void Solve()
    {
        GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(individualMatrix.NumberOfPerformers, individualMatrix.NumberOfPerformers, 0.2, 5, 20);

        ObjectiveFunction.SetData(individualMatrix.Data);

        var (geneticObjFuncValue, geneticResultDistribution) = geneticAlgorithm.Execute(true);
        Console.WriteLine("Результати генетичного алгоритму: ");
        Console.WriteLine($"\tЗначення цільової функції: {geneticObjFuncValue}");
        Console.WriteLine($"\tОтриманий розподіл: {OutputHelper.GetFormattedDistribution(geneticResultDistribution)}");
        Console.WriteLine();

        var (averageGreedyObjFuncValue, averageGreedyResultDistribution) = TripleGreedy.GetAvarageObjectiveFunction(individualMatrix.Data);
        Console.WriteLine("Результати жадібного алгоритму: ");
        Console.WriteLine($"\tЗначення цільової функції: {averageGreedyObjFuncValue}");
        Console.WriteLine($"\tОтриманий розподіл: {OutputHelper.GetFormattedDistribution(averageGreedyResultDistribution.ToArray())}");
        Console.WriteLine();

        var (tripleGreedyObjFuncValue, tripleGreedyResultDistribution) = TripleGreedy.Execute(individualMatrix.Data);
        Console.WriteLine("Результати потрійного жадібного алгоритму: ");
        Console.WriteLine($"\tЗначення цільової функції: {tripleGreedyObjFuncValue}");
        Console.WriteLine($"\tОтриманий розподіл: {OutputHelper.GetFormattedDistribution(tripleGreedyResultDistribution.ToArray())}");
    }
}
