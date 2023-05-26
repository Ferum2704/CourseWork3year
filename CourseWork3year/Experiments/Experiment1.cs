namespace CourseWork3year.Experiments;

public static class Experiment1
{
    private static int matrixSize;
    private static int[] meanValues;
    private static int[] semiIntervals;
    private static Dictionary<(int meanValue, int semiInterval), (double geneticObjFuncAvgValue, double tripleGreedyObjFuncAvgValue)> results = new();

    public static void ReadFromFile(string fileName)
    {
        string[] allParameters = File.ReadAllLines($"../../../Experiments/Data/{fileName}");

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
        meanValues = new int[3] { 50, 100, 300 };
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
        const int numberOfRuns = 20;

        foreach (var meanValue in meanValues)
        {
            foreach (var semiInterval in semiIntervals)
            {
                List<int> geneticObjFuncValues = new List<int>();
                List<int> tripleGreedyObjFuncValues = new List<int>();
                for (int i = 0; i < numberOfRuns; i++)
                {
                    TimeMatrix timeMatrix = new TimeMatrix(matrixSize);
                    timeMatrix.FillWithRandomValues(meanValue, semiInterval);
                    ObjectiveFunction.SetData(timeMatrix.Data);

                    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(matrixSize, matrixSize, 0.2, 5, 40);

                    geneticObjFuncValues.Add(geneticAlgorithm.Execute(true).objFuncValue);
                    tripleGreedyObjFuncValues.Add(TripleGreedy.Execute(timeMatrix.Data).objFuncValue);
                }
                double geneticObjFuncAvgValue = geneticObjFuncValues.Average();
                double tripleGreedyObjFuncAvgValue = tripleGreedyObjFuncValues.Average();

                results.Add((meanValue, semiInterval), (geneticObjFuncAvgValue, tripleGreedyObjFuncAvgValue));
            }
        }
    }

    public static void PrintResults()
    {
        File.WriteAllText($"../../../Experiments/Data/experiment1Results.txt", string.Empty);

        using (StreamWriter writer = new StreamWriter($"../../../Experiments/Data/experiment1results.txt"))
        {
            writer.WriteLine("\t\tGenetic Algorithm\t\tTripleGreedy Algorithm");
            foreach (var result in results)
            {
                writer.WriteLine($"({result.Key.meanValue}, {result.Key.semiInterval})\t{result.Value.geneticObjFuncAvgValue}\t\t\t\t{result.Value.tripleGreedyObjFuncAvgValue}");
            }
        }
    }
}
