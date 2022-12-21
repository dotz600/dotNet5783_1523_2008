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
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        public UpdateOrderWindow(int id)
        {
            InitializeComponent();
            InitializeFields(id);
        }

        private void Update_Order_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.ConfirmedOrder)
                    throw new BO.UpdateObjectFailedException("Order allredy sent");
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Sent)
                    bl!.Order.UpdateShipping(int.Parse(textBoxUpdateOrderID.Text));
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Provided)
                    bl!.Order.UpdateDelivery(int.Parse(textBoxUpdateOrderID.Text));
            }
            catch(UpdateObjectFailedException ex)
            {
                MessageBox.Show(ex.Message, ex.Message , MessageBoxButton.OK, MessageBoxImage.Hand
                                , MessageBoxResult.Cancel, MessageBoxOptions.RtlReading);
            }
        }
        private void InitializeFields(int id)
        {
            BO.Order order = bl!.Order.Read(id);//The object definitely exists
            textBoxUpdateOrderID.Text = order.ID.ToString();
            textBoxUpdateOrderCostumerName.Text = order.CustomerName;
            textBoxUpdateOrderCostumerEmail.Text = order.CustomerEmail;
            textBoxUpdateOrderDate.Text = order.OrderDate.ToString();
            textBoxUpdatePaymentDate.Text = order.PaymentDate.ToString();
            textBoxUpdateShipDate.Text = order.ShipDate != null ? order.ShipDate.ToString() : null;
            textBoxUpdateDeliveryDate.Text = order?.DeliveryDate != null ? order.DeliveryDate.ToString() : null;
            textBoxUpdateOrderPrice.Text = order?.TotalPrice != null ? order.TotalPrice.ToString() : null;
            StatusComboBoxUpdateOrder.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            StatusComboBoxUpdateOrder.SelectedItem = order?.Status;
            ItemsComboBoxUpdateOrder.ItemsSource = order?.Items;
        }
    }
}
