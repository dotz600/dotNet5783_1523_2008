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

namespace PL.BoEntityWindows.Admin
{
    /// <summary>
    /// show order details 
    /// admin can update status
    /// buyer only can watch
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();

        public UpdateOrderWindow(int id, bool flag)//flag is for to determine if its a buyer or a admin
        {
            InitializeComponent();
            UpdateProductGrid.DataContext = bl!.Order.Read(id);//insert order data to dataContext
            InitializeFields();//set the status value to show

            StatusComboBoxUpdateOrder.IsEnabled = flag;
            cofirmUpdate.IsEnabled = flag;
        }
        private void Update_Order_Confirmation_Click(object sender, RoutedEventArgs e)//will update the shipping details
        {
            try
            {
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.ConfirmedOrder)
                    throw new BO.UpdateObjectFailedException("Order allredy Confirm");//all the orders are confirm allredy 
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Sent)
                    bl!.Order.UpdateShipping(int.Parse(textBoxUpdateOrderID.Text));
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Provided)
                    bl!.Order.UpdateDelivery(int.Parse(textBoxUpdateOrderID.Text));

            }
            catch(UpdateObjectFailedException ex)
            {
                MessageBox.Show(ex.Message, "Exception" , MessageBoxButton.OK, MessageBoxImage.Hand
                                , MessageBoxResult.Cancel);
            }
            this.Close();
        }
        private void InitializeFields()//set the status value to show 
        {
            var x = Enum.GetValues(typeof(BO.OrderStatus));
            List<BO.OrderStatus> orderStatuses = new();
            foreach (BO.OrderStatus status in x)
                if(status != OrderStatus.None)
                    orderStatuses.Add(status);
            //take all the status exept None
            StatusComboBoxUpdateOrder.ItemsSource = orderStatuses;
        }
    }
}
