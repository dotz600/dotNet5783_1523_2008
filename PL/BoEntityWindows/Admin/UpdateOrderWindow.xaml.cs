
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

namespace PL.BoEntityWindows.Admin;

/// <summary>
/// show order details 
/// admin can update status
/// buyer only can watch
/// </summary>
public partial class UpdateOrderWindow : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    public bool IsEnable { get; set; }
    public BO.Order OrderDetails { get; set; }
    public List<BO.OrderStatus> StatusList //take all the order status exept none
    {
        get
        {
            var x = Enum.GetValues(typeof(BO.OrderStatus));
            List<BO.OrderStatus> res = new();
            foreach (var status in x)
                if (status.ToString() != BO.OrderStatus.None.ToString())
                    res.Add((BO.OrderStatus)status);
            return res;
        }
    }
    public UpdateOrderWindow(int id, bool flag)//flag is for to determine if its a buyer or a admin
    {
        OrderDetails = new();
        OrderDetails = bl!.Order.Read(id);//insert order data to dataContext
        IsEnable = flag;
        InitializeComponent();
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
        catch(BO.UpdateObjectFailedException ex)
        {
            MessageBox.Show(ex.Message, "Exception" , MessageBoxButton.OK, MessageBoxImage.Hand
                            , MessageBoxResult.Cancel);
        }
        this.Close();
    }

}
