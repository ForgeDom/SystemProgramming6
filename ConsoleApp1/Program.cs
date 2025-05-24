namespace SystemProgramming6;

class Program
{
    static Semaphore semaphore = new Semaphore(3, 3);

    static void Main()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadWork);
            thread.Start(i + 1);
        }

        Console.ReadKey();
    }

    static void ThreadWork(object threadNumber)
    {
        semaphore.WaitOne();

        try
        {
            Console.WriteLine($"Потік #{threadNumber} (ID: {Thread.CurrentThread.ManagedThreadId}) почав роботу.");

            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Потік #{threadNumber}: {rnd.Next(1, 100)}");
                Thread.Sleep(200);

                Console.WriteLine($"Потік #{threadNumber} завершив роботу.");
            }
        }
        finally
        {
            semaphore.Release();
        }
    }
}