namespace SystemProgramming6;

class Program
{
    static void Main()
    {
        bool isNewInstance;

        using (Mutex mutex = new Mutex(true, "Global\\MyUniqueConsoleAppMutex", out isNewInstance))
        {
            if (!isNewInstance)
            {
                Console.WriteLine("Додаток вже запущено. Друга копія не дозволена.");
                return;
            }

            Console.WriteLine("Додаток запущено. Натисніть Enter для виходу...");
            Console.ReadLine(); 
        }
    }

}