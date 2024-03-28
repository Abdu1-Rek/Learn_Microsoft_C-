using System;
using System.Linq;

class Program
{
	static void Main(string[] args)
	{
		
		char C = 'a';
		string[] A = {"abffgafds", "abdfbfbda", "agsdfgc", "abdsfsdva", "bcbdbfdb", "bsdfgfgdbac", "a", "aaa", "aba"};

		int count = A.Count(s => s.Length > 1 && s[0] == C && s[s.Length - 1] == C);

		Console.WriteLine(count);
	}
}

//Дан символ С и строковая последовательность A. 
//Найти количество элементов A, которые содержат более 
//одного символа и при этом начинаются и оканчиваются символом C.