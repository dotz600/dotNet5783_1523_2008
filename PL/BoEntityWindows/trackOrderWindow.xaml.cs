using BlApi;
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
    /// Interaction logic for trackOrderWindow.xaml
    /// </summary>
    public partial class trackOrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public trackOrderWindow(int id = 0)
        {
            InitializeComponent();
            if(id != 0)
            {
                var x = (bl.Order.TrackingOrder(id));
                ID.Content = "ID : " + x.ID.ToString();
                Status.Content = "Status : " + x.Status.ToString();
                confirmed.Content = x.Events[0].os.ToString() + ": " + x.Events[0].dt.ToString();
                if (x.Events.Count > 1)
                    sent.Content = x.Events[1].os.ToString() + ": " + x.Events[1].dt.ToString();
                if (x.Events.Count > 2)
                    provided.Content = x.Events[2].os.ToString() + ": " + x.Events[2].dt.ToString();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tmp = IdToTrack.Text;
       
            try
            {
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input! Try Agian", "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
         , MessageBoxResult.Cancel);
            }
            try
            {
                int id = int.Parse(tmp);

                var x = (bl?.Order.TrackingOrder(id));
                ID.Content = "ID : " + x?.ID.ToString();
                Status.Content = "Status : " + x?.Status.ToString();
                confirmed.Content = x?.Events[0].os.ToString() + ": " + x?.Events[0].dt.ToString();
                if (x?.Events.Count > 1)
                    sent.Content = x?.Events[1].os.ToString() + ": " + x?.Events[1].dt.ToString();
                if (x?.Events.Count > 2)
                    provided.Content = x?.Events[2].os.ToString() + ": " + x?.Events[2].dt.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
         , MessageBoxResult.Cancel);
            }
        }
    }
}
