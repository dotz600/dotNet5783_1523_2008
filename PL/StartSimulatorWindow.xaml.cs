using BlApi;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace PL;

/// <summary>
/// simulator stimulat the store dayli working 
/// take the oldest order and Send/Provide it to customer
/// do it until orders end or by clicking the stop button 
/// </summary>
public partial class StartSimulatorWindow : Window
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    private Stopwatch stopWatch = new();

    private volatile bool isTimerRun;

    BackgroundWorker backroundWorker = new();

    public string ExpectedOrderDetails//show on the screen expected order status & time to end
    {
        get { return (string)GetValue(ExpectedOrderDetailsProperty); }
        set { SetValue(ExpectedOrderDetailsProperty, value); }
    }
    // Using a DependencyProperty as the backing store for ExpectedOrderDetails.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ExpectedOrderDetailsProperty =
        DependencyProperty.Register("ExpectedOrderDetails", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));

    public string CurrentOrderHandle//show on the screen current order status & the time started 
    {
        get { return (string)GetValue(CurrentOrderHandleProperty); }
        set { SetValue(CurrentOrderHandleProperty, value); }
    }
    // Using a DependencyProperty as the backing store for CurrentOrderHandle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentOrderHandleProperty =
        DependencyProperty.Register("CurrentOrderHandle", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));
    
    
    public string ClockText
    {
        get { return (string)GetValue(ClockTextProperty); }
        set { SetValue(ClockTextProperty, value); }
    } 
    // Using a DependencyProperty as the backing store for ClockText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClockTextProperty =
        DependencyProperty.Register("ClockText", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));


    public StartSimulatorWindow()
    {
        InitializeComponent();
        ClockText = "00:00:00";

        backroundWorker.DoWork += Work;
        backroundWorker.ProgressChanged += UpdateScreen;
       // backroundWorker.RunWorkerCompleted += Timerworker_RunWorkerCompleted;
        backroundWorker.RunWorkerCompleted += closeHandler;

        Simulator.Simulator.ScreenUpdate += Simulator_ScreenUpdate;//regester update screen and stop function to Simulator class event
        Simulator.Simulator.StopSimu += Simulator_StopSimu;
        
        backroundWorker.WorkerReportsProgress = true;
        backroundWorker.WorkerSupportsCancellation = true;

        stopWatch.Start();
        isTimerRun = true;
        backroundWorker.RunWorkerAsync();
    }

    private void Simulator_StopSimu()//shot down - event rise from simulator, becouse there is no more order to handel
    {
        backroundWorker.CancelAsync();
        MessageBox.Show(@"No more orders to work on! Have a nice day", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }


    private void Simulator_ScreenUpdate(int x, int time, BO.Order order)//handel the event the wake in Simulator class
    {
        Tuple<BO.Order,int> t = new Tuple<BO.Order, int>(order, time);//Tuple order and random time, then send it to ReportProgress 
        backroundWorker.ReportProgress(x,t);
    }

    private void Work(object? sender, DoWorkEventArgs? e)//regester to do work - do all the work that need to be done
    {
        Simulator.Simulator.Activate();
        while (!backroundWorker.CancellationPending)//handle clock
        {
            backroundWorker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }


    
    private void UpdateScreen(Object? sender, ProgressChangedEventArgs? e)
    {
        //update the screen - call from ReportProgress - if want to update order details send order id, for clock update send 1
        if (e?.ProgressPercentage > 1000)//want to update order details
        {
            var args = (Tuple<BO.Order, int>)e.UserState!;//extract the Tuple that contain Random time to end and the order details

            //update current order handle details 
            CurrentOrderHandle = "ID : " + args.Item1.ID +
            "\nCurrent status : " + args.Item1.Status;
            
            //extract start time and end time
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            string endTime = (stopWatch.Elapsed + TimeSpan.FromSeconds(args.Item2/1000)).ToString().Substring(0,8);

            //update expected time & status of order after work will done
            ExpectedOrderDetails = "Started at : " + timerText.ToString() +
                "\nExpected processing end time : " + endTime +
                "\nWill be " + (args.Item1.Status == BO.OrderStatus.Sent ? "Provide." : "Sent.");
        }
        else if (e?.ProgressPercentage == 1)//clock update
        {
            string timerText = stopWatch.Elapsed.ToString();
            ClockText = timerText.Substring(0, 8);
        }

    }

    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        backroundWorker.CancelAsync();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (backroundWorker.CancellationPending == false && isTimerRun) // Won't allow to cancel the window!!! It is not me!!!
        {
            e.Cancel = true;
            MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
    private void closeHandler(object? sender, RunWorkerCompletedEventArgs? e)//close the window with soft thread cancel
    {
        Simulator.Simulator.DeAcitavet();
        isTimerRun = false;
        backroundWorker.DoWork -= Work;
        backroundWorker.ProgressChanged -= UpdateScreen;

        MessageBox.Show(@"Hope you enjoyed!", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        Close();
    }

    
}
