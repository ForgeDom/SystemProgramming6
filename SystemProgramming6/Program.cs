namespace SystemProgramming6;

class Program
{
    static int[] data = { 3, 8, 1, 4, 7 };
    static Mutex mutex = new Mutex();
    static bool modificationDone = false;

    static void Main()
    {
        Thread modifierThread = new Thread(ModifyArray);
        Thread maxFinderThread = new Thread(FindMax);

        modifierThread.Start();
        maxFinderThread.Start();

        modifierThread.Join();
        maxFinderThread.Join();

        Console.WriteLine("Програма завершила роботу.");
    }

    static void ModifyArray()
    {
        mutex.WaitOne(); 
        Console.WriteLine("Потік 1: модифікація масиву...");
        for (int i = 0; i < data.Length; i++)
        {
            data[i] += 5;
            Console.Write($"{data[i]} ");
            Thread.Sleep(100);
        }
        Console.WriteLine("\nМодифікація завершена");
        modificationDone = true;
        mutex.ReleaseMutex();
    }

    static void FindMax()
    {
        while (!modificationDone)
        {
            Thread.Sleep(50); 
        }

        mutex.WaitOne(); 
        Console.WriteLine("🔍 Потік 2: пошук максимуму...");
        int max = data[0];
        foreach (int value in data)
        {
            if (value > max) max = value;
        }
        Console.WriteLine($"Максимальне значення: {max}");
        mutex.ReleaseMutex();
    }
}