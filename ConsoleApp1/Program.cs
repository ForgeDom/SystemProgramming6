namespace ConsoleApp1;

class Program
{
    static Mutex mutex = new Mutex();
    static bool firstThreadFinished = false;

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(PrintAscending);
        Thread thread2 = new Thread(PrintDescending);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Обидва потоки завершили роботу.");
    }

    static void PrintAscending()
    {
        mutex.WaitOne(); 

        Console.WriteLine("Перший потік: Зростаючі числа:");
        for (int i = 0; i <= 20; i++)
        {
            Console.Write(i + " ");
            Thread.Sleep(100); 
        }

        Console.WriteLine("\nПерший потік завершено.");
        firstThreadFinished = true;

        mutex.ReleaseMutex(); 
    }

    static void PrintDescending()
    {
        while (!firstThreadFinished)
        {
            Thread.Sleep(50); 
        }

        mutex.WaitOne(); // Входимо в критичну секцію

        Console.WriteLine("Другий потік: Спадні числа:");
        for (int i = 10; i >= 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(100);
        }

        Console.WriteLine("\nДругий потік завершено.");

        mutex.ReleaseMutex();
    }
}