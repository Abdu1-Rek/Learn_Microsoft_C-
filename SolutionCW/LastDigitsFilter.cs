/*
Дана целочисленная последовательность. 
Обрабатывая только положительные числа, 
получить последовательность их последних цифр 
и удалить в полученной последовательности все вхождения одинаковых цифр, 
кроме первого. Порядок полученных цифр должен соответствовать порядку исходных чисел.
*/
 
using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		int[] numbers = { 123, 456, 789, -12, -34, 567, 890 };

		// Отфильтровываем положительные числа
		var positiveNumbers = numbers.Where(x => x > 0);

		// Получаем последние цифры каждого числа
		var lastDigits = positiveNumbers.Select(x => x % 10);

		// Удаляем все вхождения одинаковых цифр, кроме первого
		var uniqueLastDigits = lastDigits.Distinct();

		var b = numbers.Where(x => x > 0).Select(x => x % 10).Distinct();
		// Выводим результат
		Console.WriteLine(string.Join(" ", b));
	}
}
