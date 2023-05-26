using CourseWork3year;

TimeMatrix matrix = new TimeMatrix(8);
matrix.FillWithRandomValues(50, 30);
matrix.Print();


List<int> sizes = new List<int>() { 20, 50, 70, 100, 150, 230};

AlgorithmsComparator comparator = new AlgorithmsComparator(sizes, 50, 30);

comparator.ShowParametersInfluence();



/*ObjectiveFunction.SetData(matrix.Data);

GeneticAlgorithm algorithm = new GeneticAlgorithm(8, matrix.NumberOfTasks, 0.2, 5, 20);

(var objFuncValue, var distribution) = algorithm.Execute(true);

Console.WriteLine(objFuncValue + "\n\n");
foreach (var element in distribution)
{
    Console.Write(element + " ");
}

Console.OutputEncoding = Encoding.UTF8;

Experiment1.FillFileWithData();
Experiment2.FillFileWithData();
Experiment3.FillFileWithData();

Console.WriteLine($"Експеримент {1}. Виберіть спосіб задання параметрів задачі:");

Console.WriteLine("1. Зчитати з файлу. Формат:\n\t1 рядок - це розмірність квадратної матриці," +
    "\n\t2 рядок - перелік значень математичного сподівання," +
    "\n\t3 рядок - перелік значень напівінтервалів");
Console.WriteLine("2. Ввести вручну");
Console.WriteLine("3. Генерувати випадковим чином");
Console.Write("Ваш вибір: ");

int choice = int.Parse(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.Write("Введіть назву файлу: ");
        string fileName = Console.ReadLine();
        Experiment1.ReadFromFile(fileName);
        break;
    case 2:
        Experiment1.InputManually();
        break;
    case 3:
        Experiment1.SetInternally();
        break;
    default:
        Console.WriteLine("Некоректний вибір. Перезапустіть програму або перейдість до наступного експерименту");
        break;
}

Console.WriteLine($"\nЕксперимент {2}. Виберіть спосіб задання квадратної матриці часів:");

Console.WriteLine("1. Зчитати з файлу. Формат:\n\tРядок файлу - це рядок матриці" +
    "\n\tЕлементи матриці розділені пробілами \n\tЯкщо матриць декілька вони мають бути розділені пустим рядком");
Console.WriteLine("2. Ввести вручну");
Console.WriteLine("3. Генерувати випадковим чином");
Console.Write("Ваш вибір: ");

choice = int.Parse(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.Write("Введіть назву файлу: ");
        string fileName = Console.ReadLine();
        Experiment2.ReadFromFile(fileName);
        break;
    case 2:
        Experiment2.InputManually();
        break;
    case 3:
        Experiment2.Generate();
        break;
    default:
        Console.WriteLine("Некоректний вибір. Перезапустіть програму або перейдість до наступного експерименту");
        break;
}


Console.WriteLine($"\nЕксперимент {3}. Виберіть спосіб задання квадратної матриці часів:");

Console.WriteLine("1. Зчитати з файлу. Формат:\n\tРядок файлу - це рядок матриці" +
    "\n\tЕлементи матриці розділені пробілами \n\tЯкщо матриць декілька вони мають бути розділені пустим рядком");
Console.WriteLine("2. Ввести вручну");
Console.WriteLine("3. Генерувати випадковим чином");
Console.Write("Ваш вибір: ");

choice = int.Parse(Console.ReadLine());

switch (choice)
{
    case 1:
        Console.Write("Введіть назву файлу: ");
        string fileName = Console.ReadLine();
        Experiment2.ReadFromFile(fileName);
        break;
    case 2:
        Experiment2.InputManually();
        break;
    case 3:
        Experiment2.Generate();
        break;
    default:
        Console.WriteLine("Некоректний вибір. Перезапустіть програму");
        break;
}*/

