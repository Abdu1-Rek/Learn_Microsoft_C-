using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		int L1 = 3;
		int L2 = 4;
		string[] A = {"ABC", "DEF", "GHI"};
		string[] B = {"JKLM", "NOPQR", "STUV"};

		var result = A.Where(a => a.Length == L1)
					  .Concat(B.Where(b => b.Length == L2))
					  .OrderByDescending(s => s);

		foreach (string s in result)
		{
			Console.WriteLine(s);
		}
	}
}

/* 
Даны целые положительные числа L1 и L2 и строковые последовательности A и B. 
Строки последовательностей содержат только цифры и заглавные буквы латинского алфавита. 
Получить последовательность, содержащую все строки из A длины L1 и все строки из B длины L2. 
Отсортировать полученную последовательность в лексикографическом порядке по убыванию.
Concat , Join, GroupJoin, DefaultIfEmpty,GroupBy
*/