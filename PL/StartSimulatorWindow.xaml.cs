using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace PL;

/// <summary>
/// show the stimulation 
/// </summary>
public partial class StartSimulatorWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    public int IdOrderInProgress { get { return bl?.Order.GetOrderForHandle() ?? 
                                    throw new BO.ObjectNotExistException("no more order to work on"); } }

    public static readonly DependencyProperty IdProp = DependencyProperty.Register(nameof(IdOrderInProgress), typeof(int), typeof(StartSimulatorWindow));
    int myID { get => (int)GetValue(IdProp); set => SetValue(IdProp, value); }

    public BO.Order OrderInProgress { get; set; } = new();
   
    private Stopwatch stopWatch;

    private volatile bool  isTimerRun;

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
        timerworker.ProgressChanged += myFunc;
        timerworker.RunWorkerCompleted += closeHandler;

        timerworker.WorkerReportsProgress = true;
        timerworker.WorkerSupportsCancellation = true;
        stopWatch.Start();
        isTimerRun = true;


        timerworker.RunWorkerAsync();
    }
     
    private void closeHandler(object? sender, RunWorkerCompletedEventArgs? e)
    {
        timerworker.ProgressChanged -= ClockTxt;
        timerworker.ProgressChanged -= myFunc;
        MessageBox.Show(@"Hope you enjoyed!", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Close();
    }

    private void ClockRun(object? sender, DoWorkEventArgs? e)
    {
        stopWatch.Start();
        while (!timerworker.CancellationPending)
        {
            timerworker.ReportProgress(231);
            Thread.Sleep(1000);
            updateOrder();
        }

    }
    private void myFunc(object? sender, ProgressChangedEventArgs? e)
    {
        A.Content = "ID : " + IdOrderInProgress;
        B.Content = "Current status : " + OrderInProgress.Status;
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        C.Content = "Started at : " + timerText.ToString();
        D.Content = "Expected processing end time : " + (DateTime.Now + new TimeSpan(2000)).ToString();
        E.Content = "Will be " + (OrderInProgress.Status == BO.OrderStatus.Sent ? "Provide." : "Sent.");

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
        timerworker.CancelAsync();
        isTimerRun = false;
       // this.Close();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (timerworker.CancellationPending == false && isTimerRun) // Won't allow to cancel the window!!! It is not me!!!
        {
            e.Cancel = true;
            MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
