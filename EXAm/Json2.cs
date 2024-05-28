/*
Для выполнения задачи, нужно написать программу на C#, которая будет выполнять следующие шаги:
1. Получить список картинок в формате JSON с указанного URL.
2. Распарсить JSON для извлечения URL миниатюр (thumbnailUrl).
3. Скачать изображения по этим URL с использованием многопоточности или асинхронного программирования.
4. Включить троттлинг (лаг) между загрузками, чтобы избежать отказа в обслуживании.

Для выполнения этой задачи мы будем использовать:
- `HttpClient` для асинхронного HTTP-запроса.
- `JsonDocument` для парсинга JSON.
- Асинхронное программирование с `async` и `await`.
- Многопоточность с `Task` и `Parallel.ForEach`.

Вот пример кода для реализации этой задачи:
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	private static readonly HttpClient client = new HttpClient();
	private const string url = "https://jsonplaceholder.typicode.com/photos";
	private const string downloadFolder = "DownloadedThumbnails";

	static async Task Main(string[] args)
	{
		try
		{
			// Получение списка картинок
			var json = await client.GetStringAsync(url);
			var photos = JsonDocument.Parse(json).RootElement.EnumerateArray()
				.Select(photo => new
				{
					Id = photo.GetProperty("id").GetInt32(),
					Title = photo.GetProperty("title").GetString(),
					ThumbnailUrl = photo.GetProperty("thumbnailUrl").GetString()
				}).ToList();

			Console.WriteLine($"Found {photos.Count} photos.");

			// Создание папки для загрузки
			if (!Directory.Exists(downloadFolder))
			{
				Directory.CreateDirectory(downloadFolder);
			}

			// Скачивание картинок с троттлингом
			var tasks = photos.Select((photo, index) => Task.Run(async () =>
			{
				await Task.Delay(index * 100); // Троттлинг: 100 мс задержка между запросами
				await DownloadImageAsync(photo.ThumbnailUrl, photo.Id);
			})).ToArray();

			await Task.WhenAll(tasks);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	private static async Task DownloadImageAsync(string imageUrl, int imageId)
	{
		try
		{
			var response = await client.GetAsync(imageUrl);
			response.EnsureSuccessStatusCode();

			var imageBytes = await response.Content.ReadAsByteArrayAsync();
			var filePath = Path.Combine(downloadFolder, $"{imageId}.jpg");

			await File.WriteAllBytesAsync(filePath, imageBytes);

			Console.WriteLine($"Downloaded image {imageId}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to download image {imageId}: {ex.Message}");
		}
	}
}

/*
1. По адресу https://jsonplaceholder.typicode.com/photos получить список картинок в виде json. 
Реализовать скачивание картинок по адресу thumbnailUrl. Использовать максимально все технологии, 
которые вы знаете: многопоточное или асинхронное программирование, linq и т.д. 
Парсер json можно использовать стандартный (https://zetcode.com/csharp/json/).
Скачивать лучше не непрерывно, а с небольшим троттлингом(лагом), чтобы не получить отказ в обслуживании
### Пояснения:
1. **HttpClient:** Используем для выполнения HTTP-запросов.
2. **JsonDocument:** Используется для парсинга JSON и извлечения необходимых данных.
3. **Task и async/await:** Асинхронное программирование используется для выполнения загрузок.
4. **Task.Run и Task.Delay:** Для обеспечения троттлинга между запросами.
5. **Directory и File:** Создание папки и запись файлов.

Этот код скачивает изображения по адресу `thumbnailUrl`, сохраняет их в папке `DownloadedThumbnails`, 
и выводит в консоль информацию о загрузке. Троттлинг осуществляется задержкой в 100 миллисекунд 
между началом загрузки каждого изображения.
*/