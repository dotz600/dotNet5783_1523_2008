using BlApi;
using PL.BoEntityWindows.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.BoEntityWindows;

/// <summary>
/// take from buyer order id to track and go to order details window 
/// open from main window
/// </summary>
public partial class trackOrderWindow : Window
{
    public trackOrderWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
   
        try
        {
            int id = int.Parse(IdToTrack.Text);//convert id to int
            this.Close();
            new UpdateOrderWindow(id, false).Show();
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
            , MessageBoxResult.Cancel);
        }
    }
}
