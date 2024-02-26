using.System;
internal class Program
{
	private static void Main()
	{
		Random dice = new Random();
		int roll = dice.Next(1, 7);
		Console.WriteLine(roll);
	}
}