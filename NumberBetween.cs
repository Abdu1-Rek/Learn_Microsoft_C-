Console.WriteLine("Enter an integer value between 5 and 10");

string? readResult;
string valueEntered = "";
int numValue = 0;
bool validNumber = false;

do
{
	readResult = Console.ReadLine();
	if (readResult != null)
	{
		valueEntered = readResult;
	}
	
	validNumber = int.TryParse(valueEntered, out numValue);
	
	if (validNumber == true)
	{
		if(numValue < 5 || numValue > 10)
		{
			validNumber = false;
			Console.WriteLine($"Your entered {numValue}. Please enter a number between 5 and 10.");
		}
	}
	else 
	{
		Console.WriteLine("Sorry, your entered an invalid number, please try again.");
	}

} while (validNumber == false);

Console.WriteLine($"Your input value ({numValue}) has been accepted");