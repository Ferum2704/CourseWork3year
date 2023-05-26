using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork3year;

public static class ObjectiveFunction
{
    private static int[,] _data;
    private static int[] _distribution;

    public static void SetData(int[,] data)
    {
        _data = data;
    }

    public static void SetDistribution(int[] distribution)
    {
        _distribution = distribution;
    }

    public static int Calculate() =>
        GetMaxElementFromDistribution() - GetMinElementFromDistribution();

    private static int GetMaxElementFromDistribution()
    {
        int maxElement = 0;
        for (int i = 0; i < _distribution.Length; i++)
        {
            int el = _data[i, _distribution[i]-1];
            if (el > maxElement)
            {
                maxElement = el;
            }
        }
        return maxElement;
    }

    private static int GetMinElementFromDistribution()
    {
        int minElement = int.MaxValue;
        for (int i = 0; i < _distribution.Length; i++)
        {
            int el = _data[i, _distribution[i]-1];
            if (el < minElement)
            {
                minElement = el;
            }
        }
        return minElement;
    }
}
