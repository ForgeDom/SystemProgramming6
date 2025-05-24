using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SystemProgramming6;

public partial class MainWindow : Window
{
    static int[] data = { 3, 8, 1, 4, 7 };
    static Mutex mutex = new Mutex();
    static bool modificationDone = false;

    StringBuilder log = new StringBuilder();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartThreads_Click(object sender, RoutedEventArgs e)
    {
        data = new int[] { 3, 8, 1, 4, 7 };
        modificationDone = false;
        log.Clear();
        OutputBox.Text = "";

        Thread modifierThread = new Thread(ModifyArray);
        Thread maxFinderThread = new Thread(FindMax);

        modifierThread.Start();
        maxFinderThread.Start();
    }

    void ModifyArray()
    {
        mutex.WaitOne();
        Append("Потік 1: модифікація масиву...");

        for (int i = 0; i < data.Length; i++)
        {
            data[i] += 5;
            Append($"data[{i}] = {data[i]}");
            Thread.Sleep(100);
        }

        Append("Модифікація завершена");
        modificationDone = true;
        mutex.ReleaseMutex();
    }

    void FindMax()
    {
        while (!modificationDone)
        {
            Thread.Sleep(50);
        }

        mutex.WaitOne();
        Append("Потік 2: пошук максимуму...");
        int max = data[0];
        foreach (int value in data)
        {
            if (value > max) max = value;
        }

        Append($"Максимальне значення: {max}");
        mutex.ReleaseMutex();
    }

    void Append(string message)
    {
        Dispatcher.Invoke(() =>
        {
            log.AppendLine(message);
            OutputBox.Text = log.ToString();
            OutputBox.ScrollToEnd();
        });
    }
}