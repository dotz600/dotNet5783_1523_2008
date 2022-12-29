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

namespace PL.BoEntityWindows
{
    /// <summary>
    /// show tracking details
    /// </summary>
    public partial class trackOrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public trackOrderWindow()//can open fron order window with id to track
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       
            try
            {
                string tmp = IdToTrack.Text;
                int id = int.Parse(tmp);//convert id to int
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
}
