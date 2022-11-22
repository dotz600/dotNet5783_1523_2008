using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BIApi;
using BO;
using Dal;

namespace BIImplementation;

internal class Order : IOrder
{
    
    private DalApi.IDal dal => new Dal.DalList();

    IEnumerable<BO.Order> IOrder.ReadAll()
    {
        List<BO.Order> orders = new List<BO.Order>();
        foreach (DO.Order order in dal.Order.ReadAll())
        {
            BO.Order o =  DoToBoOrder(order);
            orders.Add(o);
        }    
        return orders;
    }

    BO.Order IOrder.Read(int orderId)
    {
        DO.Order o = dal.Order.Read(orderId);
        return DoToBoOrder(o);
    }

    private static BO.Order DoToBoOrder(DO.Order o)//recives DO.Order, returns initialized BO.Order 
    {
        return new BO.Order()
        {
            ID = o.ID,
            CustomerAdress = o.CustomerAdress
        ,
            OrderDate = o.OrderDate,
            CustomerEmail = o.CustomerEmail,
            CustomerName = o.CustomerName
        ,
            DeliveryDate = o.DeliveryrDate,
            ShipDate = o.ShipDate
        };
    }

    public void Create(BO.Order o)//creates an order of DO.Order type, and send the object to DAL
    {
        DO.Order order = BoToDoOrder(o);

        dal.Order.Create(order);
    }

    private static DO.Order BoToDoOrder(BO.Order o)//recives BO.Order, returns initialized DO.Order 
    {
        return new DO.Order()
        {
            ID = o.ID,
            CustomerAdress = o.CustomerAdress
        ,
            OrderDate = o.OrderDate,
            CustomerEmail = o.CustomerEmail,
            CustomerName = o.CustomerName
        ,
            DeliveryDate = o.DeliveryrDate,
            ShipDate = o.ShipDate
        };
    }

    void IOrder.Delete(int id)
    {
        dal.Order.Delete(id);
    }

    BO.Order IOrder.UpdateDelivery(int orderId)
    {
        throw new NotImplementedException();
    }

    BO.OrderTracking IOrder.TrackingOrder(int orderId)//??????
    {
        DO.Order o = dal.Order.Read(orderId);
        OrderStatus orderStatus = OrderStatus.ConfirmedOrder;

        //TO DO --- Payment date!!!

        if (o.ShipDate < DateTime.Now)//if true - the order has been sent and need to update orderStatus
            orderStatus = OrderStatus.Sent;
        if (o.DeliveryDate < DateTime.Now)
            orderStatus = OrderStatus.Provided;

        return new BO.OrderTracking() { ID = o.ID, Status = orderStatus };
    }

    void IOrder.UpdateShipping(int orderId)
    {
        DO.Order order = dal.Order.Read(orderId);
        order.ShipDate = DateTime.Now;
        dal.Order.Update(order);
    }
}
