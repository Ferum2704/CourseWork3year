namespace CourseWork3year.Experiments;

public static class ExperimentMode
{
    public static void Start()
    {
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

        Experiment1.Execute();
        Experiment1.PrintResults();

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

        Experiment2.Execute();

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
                Experiment3.ReadFromFile(fileName);
                break;
            case 2:
                Experiment3.InputManually();
                break;
            case 3:
                Experiment3.Generate();
                break;
            default:
                Console.WriteLine("Некоректний вибір. Перезапустіть програму");
                break;
        }

        Experiment3.Execute();
    }
}
