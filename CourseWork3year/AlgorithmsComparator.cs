using System.Diagnostics;

namespace CourseWork3year
{
    internal class AlgorithmsComparator
    {
        private const int RUNS = 20;

        private List<int> _sizes;
        private int _meanValue;
        private int _semiInterval;

        public AlgorithmsComparator(List<int> sizes, int mathematicalExpectation, int semiInterval)
        {
            _sizes = new List<int> (sizes);

            _meanValue = mathematicalExpectation;

            _semiInterval = semiInterval;
        }

        public void ShowParametersInfluence()
        {
            OutputSizeInfluenceInfo();
            OutputSizeInfluenceToPrecisionInfo();

            ExcelWriter.SaveToFile();
        }

        private void OutputSizeInfluenceToPrecisionInfo()// вплив розмірності задачі на точність роботи алгоритму
        {
            List<double> tripleGreedyValues = new List<double> ();
            List<double> avgGreedyValues = new List<double> ();
            List<double> geneticValues = new List<double> ();

            foreach (int size in _sizes)
            {
                double tripleObjectiveFunction = 0;
                double avarageObjectiveFunction = 0;
                double geneticObjectiveFunction = 0;

                for (int i = 0; i < RUNS; i++)
                {
                    TimeMatrix matrix = new TimeMatrix(size);

                    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(8, size, 0.2, 5, 20);
                    ObjectiveFunction.SetData(matrix.Data);

                    matrix.FillWithRandomValues(_meanValue, _semiInterval);

                    tripleObjectiveFunction += TripleGreedy.GetObjectiveFunction(matrix.Data);
                    avarageObjectiveFunction += TripleGreedy.GetAvarageObjectiveFunction(matrix.Data);
                    geneticObjectiveFunction += geneticAlgorithm.ExecuteObj(true);
                }

                tripleGreedyValues.Add(tripleObjectiveFunction / RUNS);
                avgGreedyValues.Add(avarageObjectiveFunction / RUNS);
                geneticValues.Add(geneticObjectiveFunction / RUNS);
            }

            ExcelWriter.OutputSizeToPrecisionComparison(tripleGreedyValues, avgGreedyValues, geneticValues,_sizes, "Розмір/Точність");
        }

        private void OutputSizeInfluenceInfo()// вплив розмірності задачі на час роботи алгоритму
        {
            Stopwatch stopWatchTripleAlgorithm = new Stopwatch();
            Stopwatch stopWatchAvarageAlgorithm = new Stopwatch();
            Stopwatch stopWatchGeneticAlgorithm = new Stopwatch();

            List<long> intervalsTripleGreedy = new List<long>();
            List<long> intervalsAvarageGreedy = new List<long>();
            List<long> intervalsGeneticAlgorithm = new List<long>();

            int tripleObjectiveFunction = 0;
            int avarageObjectiveFunction = 0;
            int geneticObjectiveFunction = 0;

            foreach (int size in _sizes)
            {
                for (int i = 0; i < RUNS; i++)
                {                   
                    TimeMatrix matrix = new TimeMatrix(size);
                    matrix.FillWithRandomValues(_meanValue, _semiInterval);

                    stopWatchTripleAlgorithm.Start();
                    tripleObjectiveFunction = TripleGreedy.GetObjectiveFunction(matrix.Data);
                    stopWatchTripleAlgorithm.Stop();

                    stopWatchAvarageAlgorithm.Start();
                    avarageObjectiveFunction = TripleGreedy.GetAvarageObjectiveFunction(matrix.Data);
                    stopWatchAvarageAlgorithm.Stop();

                    GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(8, size, 0.2, 5, 20);

                    ObjectiveFunction.SetData(matrix.Data);

                    stopWatchGeneticAlgorithm.Start();
                    geneticObjectiveFunction = geneticAlgorithm.ExecuteObj(true);
                    stopWatchGeneticAlgorithm.Stop();

                }

                intervalsTripleGreedy.Add(stopWatchTripleAlgorithm.ElapsedMilliseconds / RUNS);
                intervalsAvarageGreedy.Add(stopWatchAvarageAlgorithm.ElapsedMilliseconds / RUNS);
                intervalsGeneticAlgorithm.Add(stopWatchGeneticAlgorithm.ElapsedMilliseconds / RUNS);

                stopWatchTripleAlgorithm.Reset();
                stopWatchAvarageAlgorithm.Reset();
                stopWatchGeneticAlgorithm.Reset();
            }

            ExcelWriter.OutputSizeToTimeComparison(intervalsTripleGreedy, intervalsAvarageGreedy, intervalsGeneticAlgorithm, _sizes, "Розмір/Час");
        }
    }
}
