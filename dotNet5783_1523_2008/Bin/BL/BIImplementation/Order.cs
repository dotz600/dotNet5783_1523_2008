using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BIApi;
using BO;

namespace BIImplementation;

internal class Order :IOrder
{

    private DalApi.IDal dal => new Dal.DalList();

    public IEnumerable<BO.OrderForList> ReadAll()//returns list of BO.Order
    {
        List<BO.OrderForList> list = new List<BO.OrderForList>();//create list to return

        foreach (DO.Order order in dal.Order.ReadAll())//build and push elements to the list
        {
            int countAmountOfItems = 0;
            double sumPrice = 0;
            ///////////////*************TODO --- update status!! This line is uncorrect*****************///////////////////////

            OrderStatus status = order.OrderDate > order.DeliveryDate ? OrderStatus.Sent : OrderStatus.Provided;

            calcAmountAndPrice(ref countAmountOfItems, ref sumPrice, order.ID);

            BO.OrderForList o = new BO.OrderForList() //build single element
            {
                ID = order.ID,
                AmountOfItems = countAmountOfItems,
                CustomerName = order.CustomerName,
                TotalPrice = sumPrice,
                Status = status
            };
            list.Add(o);//push the element to the list
        }
        return list;
    }
    public BO.Order Read(int orderId)//returns single BO.Order
    {
        if (orderId > 0)//check input
        {

            try
            {
                DO.Order o;
                o = dal.Order.Read(orderId);
                BO.Order orderReturn = new BO.Order()//build order to return
                {
                    ID = o.ID,
                    CustomerAdress = o.CustomerAdress,
                    CustomerName = o.CustomerName,
                    CustomerEmail = o.CustomerEmail,
                    ShipDate = o.ShipDate,
                    DeliveryDate = o.DeliveryDate,
                    OrderDate = o.OrderDate
                };




                //calc price and amount
                int amount = 0;
                double price = 0;
                calcAmountAndPrice(ref amount, ref price, o.ID);
                //update the results
                orderReturn.TotalPrice = price;
                ///////////////*************TODO --- update status!!*****************///////////////////////
                return orderReturn;
            }//check if the order exist in DL
            catch (Exception ex) { }
            throw new Exception("BO.Order.Read");

        }
        else
            throw new Exception("invalid input");
    }
    public BO.Order UpdateDelivery(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
<<<<<<< HEAD
            order.DeliveryDate = DateTime.Now;
            BO.Order orderReturn = new BO.Order()
            {
                CustomerAdress = order.CustomerAdress
            ,
                CustomerName = order.CustomerName
            ,
                CustomerEmail = order.CustomerEmail
            ,
                DeliveryDate = order.DeliveryDate
            ,
                ID = order.ID
            ,
                OrderDate = order.OrderDate
            ,
                ShipDate = order.ShipDate
            ,
                Status = OrderStatus.Sent
            ,
                PaymentDate = order.OrderDate
            };
            orderReturn.Items = buildItemsList(orderReturn.ID);//buils items list
=======
           order.DeliveryDate = DateTime.Now;
           BO.Order orderReturn = new BO.Order()
           { CustomerAdress = order.CustomerAdress
           , CustomerName = order.CustomerName
           , CustomerEmail = order.CustomerEmail
           , DeliveryDate = order.DeliveryDate
           , ID = order.ID
           , OrderDate = order.OrderDate 
           , ShipDate = order.ShipDate
           , Status = OrderStatus.Sent
           , PaymentDate = order.OrderDate
           };
          orderReturn.Items =  buildItemsList(orderReturn.ID);//buils items list
            int temp = 0;
            double price=0;
            calcAmountAndPrice(ref temp ,ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch(Exception ex) { }
        throw new Exception("IOrder.UpdateDelivery");
    }
    public BO.Order UpdateShipping(int orderId)//update status, and returns BO.Order
    {
       try{ 
           DO.Order order = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
           order.DeliveryDate = DateTime.Now;
           BO.Order orderReturn = new BO.Order()
           { CustomerAdress = order.CustomerAdress
           , CustomerName = order.CustomerName
           , CustomerEmail = order.CustomerEmail
           , DeliveryDate = order.DeliveryDate
           , ID = order.ID
           , OrderDate = order.OrderDate 
           , ShipDate = order.ShipDate
           , Status = OrderStatus.Provided
           , PaymentDate = order.OrderDate
           };
          orderReturn.Items = buildItemsList(orderReturn.ID);//buils items list
>>>>>>> aeee344f18b206a1745caed5cb8e120bda803f48
            int temp = 0;
            double price = 0;
            calcAmountAndPrice(ref temp, ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch (Exception ex) { }
        throw new Exception("IOrder.UpdateDelivery");
    }
    public BO.Order UpdateShipping(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
            order.DeliveryDate = DateTime.Now;
            BO.Order orderReturn = new BO.Order()
            {
                CustomerAdress = order.CustomerAdress
            ,
                CustomerName = order.CustomerName
            ,
                CustomerEmail = order.CustomerEmail
            ,
                DeliveryDate = order.DeliveryDate
            ,
                ID = order.ID
            ,
                OrderDate = order.OrderDate
            ,
                ShipDate = order.ShipDate
            ,
                Status = OrderStatus.Provided
            ,
                PaymentDate = order.OrderDate
            };
            orderReturn.Items = buildItemsList(orderReturn.ID);//buils items list
            int temp = 0;
            double price = 0;
            calcAmountAndPrice(ref temp, ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch (Exception ex) { }
        throw new Exception("IOrder.UpdateShipping");
    }
    public BO.OrderTracking TrackingOrder(int orderId)//returns current status, and list of events that were occurred in these order
    {
        try
        {
            DO.Order o = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
            OrderStatus orderStatus = OrderStatus.ConfirmedOrder;

            if (o.ShipDate < DateTime.Now)//if true - the order has been sent and need to update orderStatus
                orderStatus = OrderStatus.Sent;
            if (o.DeliveryDate < DateTime.Now)
                orderStatus = OrderStatus.Provided;

            OrderTracking orderTrackingToReturn = new OrderTracking();
            OrderTracking.DateAndStatus dateAndStatus1;
            OrderTracking.DateAndStatus dateAndStatus2;
            OrderTracking.DateAndStatus dateAndStatus3;

            dateAndStatus1.dt = o.OrderDate;
            dateAndStatus1.os = OrderStatus.ConfirmedOrder;
            orderTrackingToReturn.Events.Add(dateAndStatus1);

            if (o.ShipDate < DateTime.Now)
            {
                dateAndStatus2.dt = o.ShipDate;
                dateAndStatus2.os = OrderStatus.Sent;
                orderTrackingToReturn.Events.Add(dateAndStatus2);
            }
            if (o.DeliveryDate < DateTime.Now)
            {
                dateAndStatus3.dt = o.DeliveryDate;
                dateAndStatus3.os = OrderStatus.Provided;
                orderTrackingToReturn.Events.Add(dateAndStatus3);
            }

            return orderTrackingToReturn;
        }
        catch (Exception ex) { }
        throw new Exception("BO.Order.TrackingOrder");

    }

    public List<BO.OrderItem> buildItemsList(int id)
    {
        List<BO.OrderItem> listReturn = new List<BO.OrderItem>();
        foreach (DO.OrderItem doi in dal.OrderItem.ReadAll())
        {
            if (doi.OrderID == id)//if true, build an BO.OrderItem object, and push to the list
            {
                BO.OrderItem boi = new BO.OrderItem()
                {
                    ID = doi.OrderID
                    ,
                    Amount = doi.Amount
                    ,
                    Name = dal.Product.Read(doi.ProductID).Name
                    ,
                    Price = doi.Price
                    ,
                    ProductID = doi.ProductID
                    ,
                    TotalPrice = doi.Price * doi.Amount
                };
                listReturn.Add(boi);
            }

        }
        return listReturn;
    }
    void calcAmountAndPrice(ref int countAmountOfItems, ref double price, int id)
    {
        foreach (DO.OrderItem orderItem in dal.OrderItem.ReadAll())
        {
            if (orderItem.OrderID == id)
            {
                countAmountOfItems++;
                price += orderItem.Price;
            }
        }
    }
}
