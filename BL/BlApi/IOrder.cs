using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

/// <summary>
/// Order 
/// Handles orders, implements broad functionality
/// such as returning a list of orders and updating orders.
/// Sends requests to the data layer and returns to the user interface.
/// Functions:
/// * Read all orders from DL
/// * Read one order details according to ID
/// * Update delivery date of one order according to ID
/// * Update shipping date of one order according to ID
/// * Track on one order according to ID
/// Fields:
///* ID
///* Customer Name, Email, Adress 
///* Order Date & status 
///* Payment Date 
///* ShipDate
///* DeliveryDate
///* TotalPrice
/// </summary>
public interface IOrder
{
    IEnumerable<BO.OrderForList?> ReadAll(Func<BO.OrderForList?, bool>? predicate = null);//returns list of BO.Order
    BO.Order Read(int orderId);//returns single BO.Order
    BO.Order UpdateDelivery(int orderId);//update status, and returns BO.Order
    BO.Order UpdateShipping(int orderId);//update status, and returns BO.Order
    BO.OrderTracking TrackingOrder(int orderId);//returns current status, and list of events that were occurred in these order

    int? GetOrderForHandle();
}
