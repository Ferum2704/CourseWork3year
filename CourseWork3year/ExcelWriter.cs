using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace CourseWork3year
{
    public static class ExcelWriter
    {
        private const int INDENTATION = 15;

        private static int currentRow = 1;
        private static int currentChart = 1;
        private static ExcelPackage package;
        private static ExcelWorksheet worksheet;

        static ExcelWriter()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            package = new ExcelPackage();
            worksheet = package.Workbook.Worksheets.Add("Worksheet1");
            worksheet.Column(1).Width = 20;
        }


        public static void OutputSizeToTimeComparison(List<long> tripleGreedy, List<long> avarageGreedy, List<long> geneticAlg, List<int> sizes, string message)
        {
            worksheet.Cells[currentRow, 1].Value = message;

            worksheet.Cells[++currentRow, 1].Value = "Розмірності";
            worksheet.Cells[currentRow + 1, 1].Value = "3xGreedy";
            worksheet.Cells[currentRow + 2, 1].Value = "AvgGreedy";  // for list2
            worksheet.Cells[currentRow + 3, 1].Value = "Genetic";  // for list2

            for (int j = 0; j < tripleGreedy.Count; j++)
            {
                worksheet.Cells[currentRow, j + 2].Value = sizes[j]; // X values
                worksheet.Cells[currentRow + 1, j + 2].Value = tripleGreedy[j]; // Y values for list1
                worksheet.Cells[currentRow + 2, j + 2].Value = avarageGreedy[j]; // Y values for list2
                worksheet.Cells[currentRow + 3, j + 2].Value = geneticAlg[j]; // Y values for list3
            }

            var chart = worksheet.Drawings.AddChart($"chart{currentChart}", eChartType.Line);

            // Виводимо першу криву
            var serie1 = chart.Series.Add(worksheet.Cells[currentRow + 1, 2, currentRow + 1, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie1.Header = "Час потрійного жадібного (ms)";

            // Виводимо другу криву
            var serie2 = chart.Series.Add(worksheet.Cells[currentRow + 2, 2, currentRow + 2, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie2.Header = "Час жадібного (ms)";

            var serie3 = chart.Series.Add(worksheet.Cells[currentRow + 3, 2, currentRow + 3, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie3.Header = "Час генетичного (ms)";

            chart.SetPosition(currentRow - 1, 0, tripleGreedy.Count + 2, 0);
            chart.SetSize(700, 300);

            currentRow += INDENTATION;
            currentChart++;
        }


        public static void OutputSizeToPrecisionComparison(List<double> tripleGreedy, List<double> avarageGreedy, List<double> geneticAlg, List<int> sizes, string message)
        {
            worksheet.Cells[currentRow, 1].Value = message;

            worksheet.Cells[++currentRow, 1].Value = "Розмірності";
            worksheet.Cells[currentRow + 1, 1].Value = "3xGreedy";
            worksheet.Cells[currentRow + 2, 1].Value = "AvgGreedy";
            worksheet.Cells[currentRow + 3, 1].Value = "Genetic";  // for list2

            for (int j = 0; j < tripleGreedy.Count; j++)
            {
                worksheet.Cells[currentRow, j + 2].Value = sizes[j]; // X values
                worksheet.Cells[currentRow + 1, j + 2].Value = tripleGreedy[j]; // Y values for list1
                worksheet.Cells[currentRow + 2, j + 2].Value = avarageGreedy[j]; // Y values for list2
                worksheet.Cells[currentRow + 3, j + 2].Value = geneticAlg[j]; // Y values for list3
            }

            var chart = worksheet.Drawings.AddChart($"chart{currentChart}", eChartType.Line);

            // Виводимо першу криву
            var serie1 = chart.Series.Add(worksheet.Cells[currentRow + 1, 2, currentRow + 1, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie1.Header = "Сер. відхилення потрійного жадібного";

            // Виводимо другу криву
            var serie2 = chart.Series.Add(worksheet.Cells[currentRow + 2, 2, currentRow + 2, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie2.Header = "Сер. відхилення жадібного";

            var serie3 = chart.Series.Add(worksheet.Cells[currentRow + 3, 2, currentRow + 3, tripleGreedy.Count + 1], worksheet.Cells[currentRow, 2, currentRow, tripleGreedy.Count + 1]);
            serie3.Header = "Сер. відхилення генетичного";

            chart.SetPosition(currentRow - 1, 0, tripleGreedy.Count + 2, 0);
            chart.SetSize(700, 300);

            currentRow += INDENTATION;
            currentChart++;
        }
        public static void SaveToFile()
        {
            var directory = Directory.GetParent(Environment.CurrentDirectory).FullName;
            directory = Directory.GetParent(directory).FullName;
            var fullPath = Path.Combine(Directory.GetParent(directory).FullName, "output.xlsx");

            FileInfo file = new FileInfo(fullPath);
            package.SaveAs(file);
        }
    }
}
