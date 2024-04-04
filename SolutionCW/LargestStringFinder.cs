using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		int L = 3;
		string[] A = { "ABC", "DEFGM", "GHMFI", "JKL", "MNOFKG" };
		Console.WriteLine(FindLargestString(L, A));
	}

	static string FindLargestString(int L, string[] A)
	{
		var filteredStrings = A.Where(s => s.Length == L);
		var sortedStrings = filteredStrings.OrderByDescending(s => s);
		return sortedStrings.Any()? sortedStrings.First() : string.Empty;
	}
}
/*Дано целое число L (> 0) и строковая последовательность A. 
Строки последовательности A содержат только
заглавные буквы латинского алфавита. Среди всех строк из A, 
имеющих длину L, найти наибольшую (в смысле лексикографического порядка).
 Вывести эту строку или пустую строку, если последовательность не содержит строк длины L.
 */