using System;
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
    BO.Order Read(int orderId);//returns single BO.Order
    BO.Order UpdateDelivery(int orderId);//update status, and returns BO.Order
    BO.Order UpdateShipping(int orderId);//update status, and returns BO.Order
    BO.OrderTracking TrackingOrder(int orderId);//returns current status, and list of events that were occurred in these order
    private static void buildItemsList(ref List<BO.OrderItem> l, int id);//Building Items list according to a given ID
    private static void calcAmountAndPrice(ref int countAmountOfItems, ref int price, int id);//calculates price and amount of items according to a given ID
}
