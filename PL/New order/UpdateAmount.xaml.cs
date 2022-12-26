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
    /// Interaction logic for UpdateAmount.xaml
    /// </summary>
    public partial class UpdateAmount : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        Cart Cart;
        int productId;
        public UpdateAmount(Cart c, int id)
        {
            InitializeComponent();
            Cart = c;
            productId= id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string amount = AmountToUpdate.Text;
            int num = int.Parse(amount);
            try
            {
                bl?.Cart.Update(Cart, productId, num);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Hand
              , MessageBoxResult.Cancel);
            }
            this.Close();
            new CartView(Cart).Show();
        }
    }
}
