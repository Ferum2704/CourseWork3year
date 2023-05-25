using System.Diagnostics;

namespace CourseWork3year
{
    internal class AlgorithmsComparator
    {
        private const int RUNS = 20;

        private List<int> _sizes;
        private int _mathematicalExpectation;
        private int _delta;

        public AlgorithmsComparator(List<int> sizes, int mathematicalExpectation, int delta)
        {
            _sizes = new List<int> (sizes);

            _mathematicalExpectation = mathematicalExpectation;

            _delta = delta;
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

            foreach (int size in _sizes)
            {
                double tripleObjectiveFunction = 0;
                double avarageObjectiveFunction = 0;


                for (int i = 0; i < RUNS; i++)
                {
                    int[,] matrix = RandomMatrix.Generate(size, _mathematicalExpectation, _delta);

                    tripleObjectiveFunction += TripleGreedy.GetObjectiveFunction(matrix);
                    avarageObjectiveFunction += TripleGreedy.GetAvarageObjectiveFunction(matrix);

                }

                tripleGreedyValues.Add(tripleObjectiveFunction / RUNS);
                avgGreedyValues.Add(tripleObjectiveFunction / RUNS);

            }


            ExcelWriter.OutputSizeToPrecisionComparison(tripleGreedyValues, avgGreedyValues, _sizes, "Розмір/Точність");
        }


        private void OutputSizeInfluenceInfo()// вплив розмірності задачі на час роботи алгоритму
        {
            Stopwatch stopWatchTripleAlgorithm = new Stopwatch();
            Stopwatch stopWatchAvarageAlgorithm = new Stopwatch();


            List<long> intervalsTripleGreedy = new List<long>();
            List<long> intervalsAvarageGreedy = new List<long>();

            int tripleObjectiveFunction = 0;
            int avarageObjectiveFunction = 0;


            foreach (int size in _sizes)
            {
                for (int i = 0; i < RUNS; i++)
                {
                    int[,] matrix = RandomMatrix.Generate(size, _mathematicalExpectation, _delta);

                    stopWatchTripleAlgorithm.Start();
                    tripleObjectiveFunction = TripleGreedy.GetObjectiveFunction(matrix);
                    stopWatchTripleAlgorithm.Stop();

                    stopWatchAvarageAlgorithm.Start();
                    avarageObjectiveFunction = TripleGreedy.GetAvarageObjectiveFunction(matrix);
                    stopWatchAvarageAlgorithm.Stop();



                }

                intervalsTripleGreedy.Add(stopWatchTripleAlgorithm.ElapsedMilliseconds / RUNS);
                intervalsAvarageGreedy.Add(stopWatchAvarageAlgorithm.ElapsedMilliseconds / RUNS);


                stopWatchTripleAlgorithm.Reset();
                stopWatchAvarageAlgorithm.Reset();

            }

            ExcelWriter.OutputSizeToTimeComparison(intervalsTripleGreedy, intervalsAvarageGreedy, _sizes, "Розмір/Час");
        }
    }
}
