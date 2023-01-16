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
    private BlApi.IBl? bl = BlApi.Factory.Get();
    public int? IdOrderInProgress
    {
        get { return bl.Order.GetOrderForHandle(); }
    }
    public BO.Order? OrderInProgress
    {
        get { return bl.Order.Read((int)IdOrderInProgress); }
    }
    private Stopwatch stopWatch;

    private bool isTimerRun;

    BackgroundWorker timerworker;

    public StartSimulatorWindow()
    {
        InitializeComponent();
        InitClock();
    }

    private void InitClock()
    {
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();

        timerworker.DoWork += ClockRun;
        timerworker.ProgressChanged += ClockTxt;

        timerworker.WorkerReportsProgress = true;
        stopWatch.Start();
        isTimerRun = true;


        timerworker.RunWorkerAsync();
    }
     
    void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!isTimerRun) // Won't allow to cancel the window!!! It is not me!!!
        {
            e.Cancel = true;
            MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }

    private void ClockRun(object? sender, DoWorkEventArgs? e)
    {
        stopWatch.Start();
        while (isTimerRun)
        {
            timerworker.ReportProgress(231);
            Thread.Sleep(1000);
            updateOrder();
        }
    }
    private void ClockTxt(object? sender, ProgressChangedEventArgs? e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }
    private void updateOrder()
    {
        if (OrderInProgress.ShipDate == null)
            bl.Order.UpdateShipping(OrderInProgress.ID);
        else if (OrderInProgress.DeliveryDate == null)
            bl.Order.UpdateDelivery(OrderInProgress.ID);
    }
    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        isTimerRun = false;
       // this.Close();
    }
}
