using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class OutputHelper
{
    public static void WriteListOfMatricesToFile(List<TimeMatrix> matrices, string fileName)
    {
        try
        {
            File.WriteAllText($"../../../{fileName}", string.Empty);
            using (StreamWriter writer = new StreamWriter($"../../../{fileName}"))
            {
                foreach (var matrix in matrices)
                {
                    int dimension = matrix.NumberOfTasks;

                    for (int i = 0; i < dimension; i++)
                    {
                        for (int j = 0; j < dimension; j++)
                        {
                            writer.Write(matrix[i, j] + " ");
                        }
                        writer.WriteLine();
                    }

                    writer.WriteLine(); // Роздільна порожня лінія між матрицями
                }
            }

            Console.WriteLine("Matrices written to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
        }
    }

    public static string GetFormattedDistribution(int[] distribution)
    {
        return "[" + string.Join(", ", distribution) + "]";
    }
}
