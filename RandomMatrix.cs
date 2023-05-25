namespace CourseWork3year
{
    public static class RandomMatrix
    {
        public static Random random = new Random();
        public static int[,] Generate(int n)
        {
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(1, 21); // Generates numbers between 1 and 20
                }
            }

            return matrix;
        }

        public static int[,] Generate(int n, int mathematicalExpectation, int delta)
        {
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(mathematicalExpectation - delta, mathematicalExpectation + delta + 1); // Generates numbers between 1 and 20
                }
            }

            return matrix;
        }
    }
}
