using OfficeOpenXml;
using System.IO;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace CourseWork3year
{
    internal class Program
    {


        static void Main(string[] args)
        {
            /*int n = 5;
            List<int> mathematicalExpectations = new List<int> {20, 30};
            List<int> deltas = new List<int> { 5, 7, 10 };

            int[,] matrix = RandomMatrix.Generate(n, mathematicalExpectations[0], deltas[0]);

            MatrixViewer.OutputToConsole(matrix);

            Console.WriteLine();

            Console.WriteLine(TripleGreedy.GetObjectiveFunction(matrix));*/



/*            List<int> array = new List<int>() { 15, 12, 11, 12, 10, 6, 7 };
            ExcelWriter.OutputToExcel(array, "Array1");

            array = new List<int>() { 12, 15, 9, 18, 8, 1, 2, 9, 4 };
            ExcelWriter.OutputToExcel(array, "Array2");

            ExcelWriter.SaveToFile("output.xlsx");*/

            List<int> sizes = new List<int>() { 40, 100, 150};
            int mathematicalExpectation = 20;
            int delta = 5;

            AlgorithmsComparator comparator = new AlgorithmsComparator(sizes, mathematicalExpectation, delta);
            comparator.ShowParametersInfluence();
        }
    }
}