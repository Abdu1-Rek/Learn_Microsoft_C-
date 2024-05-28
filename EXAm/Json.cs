using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestProject
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await TakeImages();
		}

		public static async Task TakeImages()
		{
			using var client = new HttpClient();
			var url = "https://jsonplaceholder.typicode.com/photos";
			client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("C#-program")));

			var res = await client.GetStringAsync(url);
			var photos = JsonSerializer.Deserialize<List<Photo>>(res);

			var adresses = photos?.Select(photo => photo.thumbnailUrl).ToList();

			var tasks = new List<Task>();
			for (var i = 0; i < 2; i++)
			{
				var task = Task.Run(async () =>
				{
					await Task.Delay(100);
					var bytes = await client.GetByteArrayAsync(adresses[i]);
					await File.AppendAllLinesAsync("photo.txt", new List<string> { Encoding.UTF8.GetString(bytes) });
				});
				tasks.Add(task);
			}

			await Task.WhenAll(tasks);
		}
		
		record Photo(int AlbumId, int Id, string title, string url, string thumbnailUrl);
	}
}

/*

1. По адресу https://jsonplaceholder.typicode.com/photos получить список картинок в виде json. 
Реализовать скачивание картинок по адресу thumbnailUrl. Использовать максимально все технологии, 
которые вы знаете: многопоточное или асинхронное программирование, linq и т.д. 
Парсер json можно использовать стандартный (https://zetcode.com/csharp/json/).
Скачивать лучше не непрерывно, а с небольшим троттлингом(лагом), чтобы не получить отказ в обслуживании




*/