using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public class TimeMatrix
{
    private readonly int[,] timeData;

    public int NumberOfTasks { get; private set; }
    public int NumberOfPerformers { get; private set; }
    public int[,] Data => timeData;

    public TimeMatrix(int numberOfTasksAndPerformers)
    {
        NumberOfTasks = numberOfTasksAndPerformers;
        NumberOfPerformers = numberOfTasksAndPerformers;
        timeData = new int[numberOfTasksAndPerformers, numberOfTasksAndPerformers];
    }

    public int this[int row, int column]
    {
        get { return timeData[row, column]; }
        set { timeData[row, column] = value; }
    }

    public void FillWithRandomValues(int meanValue, int semiInterval)
    {
        var random = new Random();

        for (int i = 0; i < NumberOfTasks; i++)
        {
            for (int j = 0; j < NumberOfPerformers; j++)
            {
                timeData[i, j] = random.Next(meanValue - semiInterval, meanValue + semiInterval);
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < NumberOfTasks; i++)
        {
            for (int j = 0; j < NumberOfPerformers; j++)
            {
                Console.Write(timeData[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("\n\n\n");
    }
}
