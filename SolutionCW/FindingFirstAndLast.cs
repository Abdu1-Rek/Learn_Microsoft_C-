using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		int[] sequence = { -1, -2, 42, 4, -5, 6, -7, 4, 21, -6, 43 };

		Console.WriteLine("Первый положительный элемент: " + sequence.Where(x => x > 0).FirstOrDefault());

		Console.WriteLine("Последний отрицательный элемент: " + sequence.Where(x => x < 0).LastOrDefault());
	}
}

/*	
Дана целочисленная последовательность, 
содержащая как положительные, 
так и отрицательные числа. 
Вывести ее первый положительный элемент
 и последний отрицательный элемент.
 */