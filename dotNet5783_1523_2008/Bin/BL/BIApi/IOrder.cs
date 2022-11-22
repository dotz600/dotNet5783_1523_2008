using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BIApi;

/// <summary>
/// IOrder --- TO DO --- Complete the interface summary
/// </summary>
public interface IOrder
{
    IEnumerable<Order> ReadAll();

    Order Read(int orderId); //for manger screen

    void Create(Order p);
    void Delete(int id);
    Order UpdateDelivery(int orderId);

    OrderTracking TrackingOrder(int orderId);
    void UpdateShipping(int orderId);
}
