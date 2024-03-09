using System.Security.Authentication;

int numbers = 100;
for(int i = 1; i <= numbers; i++)
{
	if(i % 3 == 0 && i % 5 != 0)
		Console.WriteLine($"{i} - Fizz");
	else if(i % 5 == 0 && i % 3 != 0)
		Console.WriteLine($"{i} - Buzz");
	else if(i % 5 == 0 && i % 3 == 0)
		Console.WriteLine($"{i} - FizzBuzz");
	else Console.WriteLine(i); 
}