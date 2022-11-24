﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BIApi;

/// <summary>
/// BIApi.IOrder 
/// Handles orders, implements broad functionality
/// such as returning a list of orders and updating orders.
/// Sends requests to the data layer and returns to the user interface.
/// </summary>
public interface IOrder
{
    IEnumerable<BO.OrderForList> ReadAll();//returns list of BO.Order
    BO. Read(int orderId);//returns single BO.Order
    UpdateDelivery(int orderId);//update status, and returns BO.Order
    UpdateShipping(int orderId);//update status, and returns BO.Order
    BO.OrderTracking TrackingOrder(int orderId);//returns current status, and list of events that were occurred in these order
<<<<<<< HEAD
    void buildItemsList(ref List<BO.OrderItem> l, int id);//Building Items list according to a given ID
    void calcAmountAndPrice(ref int countAmountOfItems, ref double price, int id);//calculates price and amount of items according to a given ID

=======
    //void buildItemsList(ref List<BO.OrderItem> l, int id);//Building Items list according to a given ID
    //void calcAmountAndPrice(ref int countAmountOfItems, ref double price, int id);//calculates price and amount of items according to a given ID
>>>>>>> d1785bc30cc6a7196d12015e1a78c184817276ca
}
