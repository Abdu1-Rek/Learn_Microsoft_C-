/*
Надо написать метод, который будет находить в некоторой сборке все классы, помеченные атрибутом TestFixture, 
а в нем находить методы, помеченные атрибутами SetUp, TearDown и TestFixture, а затем выполнять эти тесты, 
выводя на консоль некоторый отчет. Алгоритм выполнения тестов:
	создается объект класса, помеченного TestFixture
	для каждого метода, помеченного атрибутом Test:
		выполняется метод, помеченный SetUp
		выполняется метод, помеченный Test
		выполняется метод, помеченный TearDown
	если в рамках выполнения SetUp, Test, TearDown выкидывается исключение — тест считается проваленным и это попадает в отчет, 
	но исключение ловится и выполнение тестов продолжается
	если в рамках выполнения SetUp, Test, TearDown исключений не было — тест помечается как выполненный.
 
 
 Для реализации метода, который будет находить и запускать тесты, помеченные определенными атрибутами в сборке, 
 вам нужно воспользоваться рефлексией в C#. Приведу пример кода, который соответствует вашему запросу.

*/

using System;
using System.Linq;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class)]
public class TestFixtureAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class SetUpAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class TearDownAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class TestAttribute : Attribute { }

public class TestRunner
{
	public static void RunTests(Assembly assembly)
	{
		var testClasses = assembly.GetTypes()
			.Where(t => t.GetCustomAttributes(typeof(TestFixtureAttribute), true).Any());

		foreach (var testClass in testClasses)
		{
			var setUpMethod = testClass.GetMethods()
				.FirstOrDefault(m => m.GetCustomAttributes(typeof(SetUpAttribute), true).Any());
			var tearDownMethod = testClass.GetMethods()
				.FirstOrDefault(m => m.GetCustomAttributes(typeof(TearDownAttribute), true).Any());
			var testMethods = testClass.GetMethods()
				.Where(m => m.GetCustomAttributes(typeof(TestAttribute), true).Any());

			var testClassInstance = Activator.CreateInstance(testClass);

			foreach (var testMethod in testMethods)
			{
				try
				{
					setUpMethod?.Invoke(testClassInstance, null);
					testMethod.Invoke(testClassInstance, null);
					tearDownMethod?.Invoke(testClassInstance, null);
					Console.WriteLine($"Test {testMethod.Name} passed.");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Test {testMethod.Name} failed with exception: {ex.Message}");
				}
			}
		}
	}
}

[TestFixture]
public class ExampleTests
{
	[SetUp]
	public void Init()
	{
		Console.WriteLine("SetUp");
	}

	[TearDown]
	public void Cleanup()
	{
		Console.WriteLine("TearDown");
	}

	[Test]
	public void Test1()
	{
		Console.WriteLine("Test1");
	}

	[Test]
	public void Test2()
	{
		Console.WriteLine("Test2");
		throw new Exception("Test2 failed");
	}
}

public class Program
{
	public static void Main(string[] args)
	{
		TestRunner.RunTests(Assembly.GetExecutingAssembly());
	}
}

/*
### Пояснения:
1. **Атрибуты:** Созданы атрибуты `TestFixture`, `SetUp`, `TearDown` и `Test` для пометки классов и методов.
2. **TestRunner:** Класс, содержащий метод `RunTests`, который принимает сборку (assembly) 
и ищет в ней классы с атрибутом `TestFixture`. Внутри каждого такого класса ищутся методы с атрибутами `SetUp`, `TearDown` и `Test`.
3. **Выполнение тестов:** Для каждого тестового метода выполняются методы `SetUp`, затем сам тестовый метод, 
а потом метод `TearDown`. Если в процессе выполнения любого из этих методов возникает исключение, 
оно перехватывается и в отчете выводится, что тест провален.
4. **Пример:** Пример класса `ExampleTests`, который содержит методы для тестирования, и основной метод `Main`, 
который запускает тесты в текущей сборке.

Этот код выводит результат выполнения тестов на консоль, указывая, прошел ли тест или произошла ошибка.
*/