﻿using BO;
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
            UpdateProductGrid.DataContext = bl!.Order.Read(id);
            InitializeFields();
        }
        public OrderWindow? prevWin;
        private void Update_Order_Confirmation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.ConfirmedOrder)
                    throw new BO.UpdateObjectFailedException("Order allredy Confirm");
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Sent)
                    bl!.Order.UpdateShipping(int.Parse(textBoxUpdateOrderID.Text));
                if ((BO.OrderStatus)StatusComboBoxUpdateOrder.SelectedItem == BO.OrderStatus.Provided)
                    bl!.Order.UpdateDelivery(int.Parse(textBoxUpdateOrderID.Text));
                prevWin?.Refresh();
            }
            catch(UpdateObjectFailedException ex)
            {
                MessageBox.Show(ex.Message, "Exception" , MessageBoxButton.OK, MessageBoxImage.Hand
                                , MessageBoxResult.Cancel);
            }
                this.Close();
        }
        private void InitializeFields()
        {
            var x = Enum.GetValues(typeof(BO.OrderStatus));
            List<BO.OrderStatus> orderStatuses = new();
            foreach (BO.OrderStatus status in x)
                orderStatuses.Add(status);
            StatusComboBoxUpdateOrder.ItemsSource = from y in orderStatuses where y != OrderStatus.None select y;
        }
    }
}
