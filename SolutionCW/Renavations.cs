using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		int[] sequenceA = { 12, 34, 56, 78, 90 };
		int[] sequenceB = { 23, 45, 67, 89, 10 };

		var result = sequenceA.Zip(sequenceB, (a, b) => new { a, b })
			.Where(pair => pair.a % 10 == pair.b % 10)
			.Select(pair => $"{pair.a}-{pair.b}");

		foreach (var pair in result)
		{
			Console.WriteLine(pair);
		}
	}
}

/*Даны последовательности положительных целых чисел A и B;
 все числа в каждой последовательности различны. 
 Найти последовательность всех пар чисел, удовлетворяющих следующим условиям:
  первый элемент пары принадлежит последовательности A, второй принадлежит B,
   и оба элемента оканчиваются одной и той же цифрой.
	Результирующая последовательность называется внутренним 
	объединением последовательностей A и B по ключу,
	определяемому последними цифрами исходных чисел.
    Представить найденное объединение в виде последовательности строк, 
	содержащих первый и второй элементы пары, разделенные дефисом, 
	например, «49-129». Порядок следования пар должен определяться
	исходным порядком элементов последовательности A,
	а для равных первых элементов — порядком элементов последовательности B.*/