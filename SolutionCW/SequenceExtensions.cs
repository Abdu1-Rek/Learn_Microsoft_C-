using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// Класс для метода расширения
public static class SequenceExtensions
{
    // Метод расширения для преобразования последовательности
    public static IEnumerable<T> CombineConsecutiveEqualElements<T>(this IEnumerable<T> source)
    {
        // Группируем элементы по их значениям
        var grouped = source.GroupBy(item => item);

        // Проходим по группам и объединяем элементы с одинаковыми значениями
        foreach (var group in grouped)
        {
            // Если это первая группа в текущем значении, просто добавляем первый элемент
            if (!group.Any()) continue;

            // Добавляем первый элемент группы
            yield return group.First();

            // Если есть элементы после первого, пропускаем их
            foreach (var item in group.Skip(1))
            {
                // Не добавляем элемент, если он такой же, как предыдущий
                if (item == group.First()) continue;

                // Добавляем элемент, если он отличается от предыдущего
                yield return item;
            }
        }
    }
}

// Пример использования метода расширения
public class Example
{
    public static void Main()
    {
        var inputSequence = new[] { 1, 1, 2, 2, 2, 1, 3, 3 };
        var resultSequence = inputSequence.CombineConsecutiveEqualElements();

        // Вывод результата
        foreach (var item in resultSequence)
        {
            Console.Write($"{item}, ");
        }
        Console.WriteLine();
    }
}