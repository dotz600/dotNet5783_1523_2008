using System;
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
    public int IdOrderInProgress { get { return bl?.Order.GetOrderForHandle() ?? 
                                    throw new BO.ObjectNotExistException("no more order to work on"); } } 
    public BO.Order OrderInProgress { get; set; } = new();
   
    private Stopwatch stopWatch;

    private bool isTimerRun;

    BackgroundWorker timerworker;

    public StartSimulatorWindow()
    {
        InitializeComponent();
        OrderInProgress = bl.Order.Read(IdOrderInProgress);
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();
        InitClock();
    }

    private void InitClock()
    {
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
        {
            bl?.Order.UpdateShipping(OrderInProgress.ID);
            OrderInProgress = bl!.Order.Read(IdOrderInProgress);
        }
        else if (OrderInProgress.DeliveryDate == null)
        {
            bl?.Order.UpdateDelivery(OrderInProgress.ID);
            OrderInProgress = bl!.Order.Read(IdOrderInProgress);
        }
    }
    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        isTimerRun = false;
       // this.Close();
    }

    private void Window_Closing_1(object sender, CancelEventArgs e)
    {
        if (!isTimerRun) // Won't allow to cancel the window!!! It is not me!!!
        {
            e.Cancel = true;
            MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
