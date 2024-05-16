string orderStream = "B123,C234,A345,C15,B177,G3003,C235,B179";

string[] orders = orderStream.Split(',');
foreach (string order in orders) 
{
	if (!(order.Length == 4))
		Console.WriteLine(order + "\t- Error");
	else
		Console.WriteLine(order);
}