Random random = new Random();
int monsterHP = 10;
int personHP = 10;
int atackHP = random.Next(1, 11);

do
{
	Console.WriteLine($"{atackHP} HP damage have taken by Person to Monster and Monster have {monsterHP - atackHP} HP\n");
	monsterHP = monsterHP - atackHP;
	atackHP = random.Next(1, 11);
	if (monsterHP <= 0)
	{
		Console.WriteLine("Monster Die");
		continue;
	}

	Console.WriteLine($"{atackHP} HP damage have taken by Monster to Person and Person have {personHP - atackHP} HP\n");
	personHP = personHP - atackHP;
	atackHP = random.Next(1, 11);
	if (personHP <= 0)
	{
		Console.WriteLine("Person Die");
		continue;
	}

} while (personHP > 0 && monsterHP > 0);
