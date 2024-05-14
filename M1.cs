class Resource
{
    private object _lock = new object();
    private string _value = "new";

    public string Value
    {
        get
        {
            lock (_lock)
            {
                return _value;
            }
        }
        set
        {
            lock (_lock)
            {
                _value = value;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Resource resource = new Resource();
        // Создаём несколько потоков, которые могут читать значение Value
        for (int i = 0; i < 10; i++)
        {
            Task.Run(() =>
            {
                Console.WriteLine(resource.Value);
            });
        }

        // Создаём один поток, который может писать в Value
        Task.Run(() =>
        {
            resource.Value = "Новое значение";
        });

        Console.ReadLine();
    }
}
