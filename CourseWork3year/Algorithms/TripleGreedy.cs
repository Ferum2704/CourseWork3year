using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year
{
    internal static class TripleGreedy
    {
        public static (int objFuncValue, List<int> distribution) Execute(int[,] matrix)
        {
            (int avarageFunction, List<int> avarageDistribution) = GetAvarageObjectiveFunction(matrix);
            (int minFunction, List<int> minDistribution) = GetMinimumObjectiveFunction(matrix);
            (int maxFunction, List<int> maxDistribution) = GetMaximumObjectiveFunction(matrix);

            if (avarageFunction <= minFunction && avarageFunction <= maxFunction) 
            {
                return (avarageFunction, avarageDistribution);
            }
            else if (minFunction <= avarageFunction && minFunction <= maxFunction)
            {
                return (minFunction, minDistribution);
            }
            else
            {
                return (maxFunction, maxDistribution);
            }
        }

        public static (int, List<int>) GetAvarageObjectiveFunction(int[,] oldMatrix)
        {
            int[,] matrix = oldMatrix.Clone() as int[,];

            List<int> nearestValues = new List<int>();
            int dimension = matrix.GetLength(0);

            while (dimension > 0)
            {
                // Calculate average
                double avg = 0;
                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        avg += matrix[i, j];
                    }
                }

                avg /= (dimension * dimension);

                // Find the nearest value to the average
                int nearestValue = matrix[0, 0];
                double smallestDifference = Math.Abs(avg - nearestValue);
                int rowToRemove = 0;
                int columnToRemove = 0;

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        double currentDifference = Math.Abs(avg - matrix[i, j]);
                        if (currentDifference < smallestDifference)
                        {
                            smallestDifference = currentDifference;
                            nearestValue = matrix[i, j];
                            rowToRemove = i;
                            columnToRemove = j;
                        }
                    }
                }

                nearestValues.Add(nearestValue);

                matrix = ReduceMatrix(matrix, rowToRemove, columnToRemove);
                dimension--;
            }

            return (nearestValues.Max() - nearestValues.Min(), nearestValues);
        }

        private static (int, List<int>) GetMinimumObjectiveFunction(int[,] oldMatrix)
        {
            int[,] matrix = oldMatrix.Clone() as int[,];

            List<int> minimumValues = new List<int>();
            int dimension = matrix.GetLength(0);

            while (dimension > 0)
            {
                // Find the minimum value
                int minValue = matrix[0, 0];
                int rowToRemove = 0;
                int columnToRemove = 0;

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        if (matrix[i, j] < minValue)
                        {
                            minValue = matrix[i, j];
                            rowToRemove = i;
                            columnToRemove = j;
                        }
                    }
                }

                minimumValues.Add(minValue);

                matrix = ReduceMatrix(matrix, rowToRemove, columnToRemove);
                dimension--;
            }

            return (minimumValues.Max() - minimumValues.Min(), minimumValues);
        }

        private static (int, List<int>) GetMaximumObjectiveFunction(int[,] oldMatrix)
        {
            int[,] matrix = oldMatrix.Clone() as int[,];

            List<int> maximumValues = new List<int>();
            int dimension = matrix.GetLength(0);

            while (dimension > 0)
            {
                // Find the maximum value
                int maxValue = matrix[0, 0];
                int rowToRemove = 0;
                int columnToRemove = 0;

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        if (matrix[i, j] > maxValue)
                        {
                            maxValue = matrix[i, j];
                            rowToRemove = i;
                            columnToRemove = j;
                        }
                    }
                }

                maximumValues.Add(maxValue);

                matrix = ReduceMatrix(matrix, rowToRemove, columnToRemove);
                dimension--;
            }

            return (maximumValues.Max() - maximumValues.Min(), maximumValues);
        }

        // Create new matrix without row and column of the maximum value
        private static int[,] ReduceMatrix(int[,] matrix, int rowToRemove, int columnToRemove)
        {
            int dimension = matrix.GetLength(0);
            int[,] newMatrix = new int[dimension - 1, dimension - 1];
            for (int i = 0, iNew = 0; i < dimension; i++)
            {
                if (i == rowToRemove) continue;
                for (int j = 0, jNew = 0; j < dimension; j++)
                {
                    if (j == columnToRemove) continue;
                    newMatrix[iNew, jNew] = matrix[i, j];
                    jNew++;
                }
                iNew++;
            }
            return newMatrix;
        }
    }
}
