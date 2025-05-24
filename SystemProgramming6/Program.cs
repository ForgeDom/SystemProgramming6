namespace SystemProgramming6;

class Program
{
    static void Main()
    {
        bool isNewInstance;
        
        using (var mutex = new System.Threading.Mutex(true, "Global\\MyUniqueMutexName", out isNewInstance))
        {
            if (!isNewInstance)
            {
                Console.WriteLine("Another instance is already running.");
                return;
            }

            Console.WriteLine("This is the only instance running.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}