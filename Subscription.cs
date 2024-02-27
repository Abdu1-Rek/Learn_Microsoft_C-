Random random = new();
int daysUntilExpiration = random.Next(12);
int discountPercentage = 0;

if (daysUntilExpiration == 0)
{
	Console.WriteLine("Your subscription has expired.");
}
else if (daysUntilExpiration == 1)
{
	discountPercentage = 20;
	Console.WriteLine($"Your subscription expires within a day. \nRenew now and save {discountPercentage}%!");
}
else if (daysUntilExpiration <= 5)
{
	discountPercentage = 10;
	Console.WriteLine($"Your subscription expires in {daysUntilExpiration} days. \nRenew now and save {discountPercentage}%!");
}
//срок действия подписки пользователя истекает через 10 дней или меньше
else if (daysUntilExpiration <= 10)
{
	Console.WriteLine("Your subscription will expire soon. Renew now!");
}
//срок действия подписки пользователя истекает через пять дней или меньше
else 
{
	Console.WriteLine();
}