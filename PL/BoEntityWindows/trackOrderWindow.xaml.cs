﻿using BlApi;
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
        public trackOrderWindow(int id)
        {
            InitializeComponent();
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
}
