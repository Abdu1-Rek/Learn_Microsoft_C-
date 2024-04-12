using System.Globalization;
using System;
CultureInfo.CurrentCulture = new CultureInfo("en-US");

string[] values = {"12.3", "45", "ABC", "11", "DEF"};

decimal total = 0m;
string str = "";
foreach (string value in values)
{
	if (decimal.TryParse(value, out decimal result))
		total += result;
	else
		str += value;
}
Console.WriteLine($"Message: {str}");
Console.WriteLine($"Total: {total}");
