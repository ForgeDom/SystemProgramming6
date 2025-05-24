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
    static Mutex mutex = new Mutex();
    static bool firstThreadFinished = false;
    StringBuilder outputBuilder = new StringBuilder();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartThreads_Click(object sender, RoutedEventArgs e)
    {
        outputBuilder.Clear();
        OutputTextBox.Text = "";

        firstThreadFinished = false;

        Thread thread1 = new Thread(PrintAscending);
        Thread thread2 = new Thread(PrintDescending);

        thread1.Start();
        thread2.Start();
    }

    void PrintAscending()
    {
        mutex.WaitOne();
        AppendText("Перший потік: Зростаючі числа:");

        for (int i = 0; i <= 20; i++)
        {
            AppendText(i + " ");
            Thread.Sleep(100);
        }

        AppendText("\nПерший потік завершено.");
        firstThreadFinished = true;
        mutex.ReleaseMutex();
    }

    void PrintDescending()
    {
        while (!firstThreadFinished)
        {
            Thread.Sleep(50);
        }

        mutex.WaitOne();
        AppendText("\nДругий потік: Спадні числа:");

        for (int i = 10; i >= 0; i--)
        {
            AppendText(i + " ");
            Thread.Sleep(100);
        }

        AppendText("\nДругий потік завершено.");
        mutex.ReleaseMutex();
    }

    void AppendText(string text)
    {
        Dispatcher.Invoke(() =>
        {
            outputBuilder.AppendLine(text);
            OutputTextBox.Text = outputBuilder.ToString();
            OutputTextBox.ScrollToEnd();
        });
    }
}