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