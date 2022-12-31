using BO;
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

namespace PL.New_order
{
    /// <summary>
    /// the window will takse from user the amount he want to order
    /// and update the cart
    /// </summary>
    public partial class UpdateAmount : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();

        int productId;
        public UpdateAmount(int id)
        {
            InitializeComponent();
            productId= id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string amount = AmountToUpdate.Text;
                int num = int.Parse(amount);//convert amount to int
                bl?.Cart.Update(MainWindow.cart, productId, num);
  
                this.Close();
                //new CartView().Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
              , MessageBoxResult.Cancel);
            }
   
        }
    }
}
