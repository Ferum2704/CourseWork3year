using CourseWork3year.Experiments;
using CourseWork3year.Individual;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Виберіть режим роботи з програмою:");
Console.WriteLine("1. Розв'язок індивідуальнох задачі");
Console.WriteLine("2. Проведення експериментів");
Console.Write("Ваш вибір: ");

int mode = int.Parse(Console.ReadLine());

switch (mode)
{
	case 1:
        IndividualTaskMode.Start();
		break;
	case 2:
		ExperimentMode.Start();
		break;
	default:
		break;
}

