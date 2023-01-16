using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for StartSimulatorWindow.xaml
/// </summary>
public partial class StartSimulatorWindow : Window
{

    private Stopwatch stopWatch;

    private bool isTimerRun;

    BackgroundWorker timerworker;

    public StartSimulatorWindow()
    {
        InitializeComponent();
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();

        timerworker.DoWork += ClockRun;
        timerworker.ProgressChanged += ClockTxt;

        timerworker.WorkerReportsProgress = true;
        stopWatch.Start();
        isTimerRun = true;


        timerworker.RunWorkerAsync();
    }


    private void ClockRun(object? sender, DoWorkEventArgs? e)
    {
        stopWatch.Start();
        while (isTimerRun)
        {
            timerworker.ReportProgress(231);
            Thread.Sleep(1000);
        }
    }
    private void ClockTxt(object? sender, ProgressChangedEventArgs? e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }

    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
