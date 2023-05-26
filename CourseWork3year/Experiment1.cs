using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class Experiment1
{
    private static int matrixSize;
    private static int[] meanValues;
    private static int[] semiIntervals;

    public static void ReadFromFile(string fileName)
    {
        string[] allParameters = File.ReadAllLines($"../../../{fileName}");

        matrixSize = int.Parse(allParameters[0]);
        meanValues = allParameters[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        semiIntervals = allParameters[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
    }

    public static void InputManually()
    {
        Console.Write("Введіть розмірність квадратної матриці(кількість виконавців та завдань):");
        matrixSize = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Введіть перелік значень математичного сподівання розділених пробілами:");
        meanValues = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        Console.Write("Введіть перелік значень напівінтервалів розділених пробілами:");
        semiIntervals = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
    }

    public static void SetInternally()
    {
        Console.Write("Введіть розмірність квадратної матриці(кількість виконавців та завдань):");
        matrixSize = int.Parse(Console.ReadLine() ?? "0");
        meanValues = new int[3] {50,100,300};
        semiIntervals = new int[3] { (int)(0.6 * 50), (int)(0.4 * 100), (int)(0.2 * 300) };
    }

    public static void FillFileWithData()
    {
        File.WriteAllText($"../../../experiment1testData.txt", string.Empty);
        meanValues = new int[3] { 50, 100, 300 };
        semiIntervals = new int[3] { (int)(0.6 * 50), (int)(0.4 * 100), (int)(0.2 * 300) };
        using (StreamWriter writer = new StreamWriter($"../../../experiment1testData.txt"))
        {
            writer.WriteLine(10);
            foreach (var mean in meanValues)
            {
                writer.Write(mean + " ");
            }
            writer.WriteLine();
            foreach (var semiInterval in semiIntervals)
            {
                writer.Write(semiInterval + " ");
            }
            writer.WriteLine();
        }
    }

    public static void Execute()
    {

    }
}
