using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Введите число N:");
		int N = int.Parse(Console.ReadLine());

		double sum = Enumerable.Range(1, N).Sum(i => 1 / i);

		Console.WriteLine($"Сумма равна {sum}");
	}
}

/*
Дано целое число N (> 0). Используя методы Range и Sum,
 найти сумму 1 + (1/2) + … + (1/N) (как вещественное число).
 */