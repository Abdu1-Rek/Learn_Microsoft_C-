/* 
Дана целочисленная последовательность A. 
Сгруппировать элементы последовательности A, 
оканчивающиеся одной и той же цифрой, и на основе этой группировки получить 
последовательность строк вида «D:S», где D — ключ группировки 
(т. е. некоторая цифра, которой оканчивается хотя бы одно из чисел последовательности A), 
а S — сумма всех чисел из A, которые оканчиваются цифрой D. 
Полученную последовательность упорядочить по возрастанию ключей. 
Указание. Использовать метод GroupBy.
*/

using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		// Пример последовательности A
		int[] A = { 12, 34, 56, 78, 90, 11, 22, 33, 44, 55, 66, 77, 88, 99 };

		// Группировка элементов последовательности A по последней цифре
		var grouped = A.GroupBy(x => x % 10);

		// Получение последовательности строк вида «D:S»
		var result = grouped.Select(group => $"{group.Key}: {group.Sum()}");

		var result2 = A.GroupBy(x => x % 10).Select(group => $"{group.Key}: {group.Sum()}");
		// Вывод результата
		Console.WriteLine(string.Join("\n", result2));
	}
}
