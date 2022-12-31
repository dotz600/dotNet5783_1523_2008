using BlApi;

using System.Data;

namespace BlImplementation;

internal class Order : IOrder
{

    private readonly DalApi.IDal? Dal = DalApi.Factory.Get();


    public IEnumerable<BO.OrderForList?> ReadAll()//returns list of BO.Order
    {
        List<BO.OrderForList?> list = new();//create list to return

        foreach (DO.Order? order in Dal?.Order.ReadAll()!)//build and push elements to the list
        {
            int countAmountOfItems = 0;
            double sumPrice = 0;

            ///maybe we need to check if all the amount in order item is exist in stock and only after that order is confirm
            /// the problem is that, the truth is all the order we have in data base should be confirmed,  
            
            BO.OrderStatus status = BO.OrderStatus.ConfirmedOrder;//every order in the data base allready confirm

            if (order?.ShipDate < DateTime.Now && order?.ShipDate != null)//status update(provide or sent)
                status = BO.OrderStatus.Sent;
            if (order?.DeliveryDate < DateTime.Now && order?.DeliveryDate != null)
                status = BO.OrderStatus.Provided;

            CalcAmountAndPrice(ref countAmountOfItems, ref sumPrice, order?.ID);

            BO.OrderForList? o = new() //build single element
            {
                ID = order?.ID ?? 0,
                AmountOfItems = countAmountOfItems,
                CustomerName = order?.CustomerName,
                TotalPrice = sumPrice,
                Status = status,
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
                DO.Order o = Dal?.Order.Read(orderId) ?? throw new NullReferenceException();
                BO.Order orderReturn = new()//build order to return
                {
                    ID = o.ID,
                    CustomerAdress = o.CustomerAdress,
                    CustomerName = o.CustomerName,
                    CustomerEmail = o.CustomerEmail,
                    ShipDate = o.ShipDate,
                    PaymentDate = o.OrderDate,
                    DeliveryDate = o.DeliveryDate,
                    OrderDate = o.OrderDate,
                    Items = BuildItemsList(orderId)!
                };
                //calc price and amount
                int amount = 0;
                double price = 0;
                CalcAmountAndPrice(ref amount, ref price, o.ID);
                //update the results
                orderReturn.TotalPrice = price;

                BO.OrderStatus status = BO.OrderStatus.ConfirmedOrder;//evrey order in the data base allready confirm
                if (orderReturn.ShipDate < DateTime.Now && orderReturn.ShipDate != null)
                    status = BO.OrderStatus.Sent;
                if (orderReturn.DeliveryDate < DateTime.Now && orderReturn.DeliveryDate != null)
                    status = BO.OrderStatus.Provided;
                orderReturn.Status = status;

                return orderReturn;
            }//check if the order exist in DL
            catch (DalApi.ObjNotFoundException ex)//all the other main will catch
            {
                throw new BO.ObjectNotExistException("BO.Order.Read", ex);
            }
        }
        else
            throw new BO.NegativeIDException("invalid input");
    }
    public BO.Order UpdateDelivery(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = Dal?.Order.Read(orderId) ?? throw new NullReferenceException();//if doesn't exist, throw from DALOrder
            if (order.ShipDate == null || order.ShipDate > DateTime.Now)
                throw new BO.UpdateObjectFailedException("Send order before!");
            order.DeliveryDate = DateTime.Now;
            Dal?.Order.Update(order);

            BO.Order orderReturn = new()
            {
                CustomerAdress = order.CustomerAdress,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                DeliveryDate = order.DeliveryDate,
                ID = order.ID,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                Status = BO.OrderStatus.Provided,
                PaymentDate = order.OrderDate
            };
            orderReturn.Items = BuildItemsList(orderReturn.ID)!;//buils items list
            int temp = 0;
            double price = 0;
            CalcAmountAndPrice(ref temp, ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException("BO.Order.UpdateDelivery", ex);
        }

    }
    public BO.Order UpdateShipping(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = Dal?.Order.Read(orderId) ?? throw new NullReferenceException();//if doesn't exist, throw from DALOrder
            if (order.DeliveryDate > order.ShipDate)
                throw new BO.UpdateObjectFailedException("Order was provided!");
            order.ShipDate = DateTime.Now;
            Dal?.Order.Update(order);//update the order in data source
            BO.Order orderReturn = new()
            {
                CustomerAdress = order.CustomerAdress,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                DeliveryDate = order.DeliveryDate,
                ID = order.ID,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                Status = BO.OrderStatus.Sent,
                PaymentDate = order.OrderDate
            };
            orderReturn.Items = BuildItemsList(orderReturn.ID)!;//buils items list
            int temp = 0;
            double price = 0;
            CalcAmountAndPrice(ref temp, ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch (DalApi.ObjNotFoundException ex)//all the other main will catch
        {
            throw new BO.ObjectNotExistException("BO.Order.UpdateShipping", ex);
        }
       
    }
    public BO.OrderTracking TrackingOrder(int orderId)//returns current status, and list of events that were occurred in these order
    {
        try
        {
            DO.Order o = Dal?.Order.Read(orderId) ?? throw new NullReferenceException();//if doesn't exist, throw from DALOrder
            BO.OrderStatus orderStatus = BO.OrderStatus.ConfirmedOrder;

            if (o.ShipDate < DateTime.Now && o.ShipDate != DateTime.MinValue)//if true - the order has been sent and need to update orderStatus
                orderStatus = BO.OrderStatus.Sent;
            if (o.DeliveryDate < DateTime.Now && o.DeliveryDate != DateTime.MinValue)
                orderStatus = BO.OrderStatus.Provided;

            BO.OrderTracking orderTrackingToReturn = new() { ID = orderId, Status = orderStatus };
          
            BO.OrderTracking.DateAndStatus dateAndStatus1, dateAndStatus2 , dateAndStatus3;//for the event list

            dateAndStatus1.dt = o.OrderDate;
            dateAndStatus1.os = BO.OrderStatus.ConfirmedOrder;

            orderTrackingToReturn.Events ??= new List<BO.OrderTracking.DateAndStatus>();//make new list

            orderTrackingToReturn.Events.Add(dateAndStatus1);

            //checl if have more events and add them to the list
            if (o.ShipDate < DateTime.Now && o.ShipDate != DateTime.MinValue)
            {
                dateAndStatus2.dt = o.ShipDate;
                dateAndStatus2.os = BO.OrderStatus.Sent;
                orderTrackingToReturn.Events.Add(dateAndStatus2);
            }
            if (o.DeliveryDate < DateTime.Now && o.DeliveryDate != DateTime.MinValue)
            {
                dateAndStatus3.dt = o.DeliveryDate;
                dateAndStatus3.os = BO.OrderStatus.Provided;
                orderTrackingToReturn.Events.Add(dateAndStatus3);
            }
            
            return orderTrackingToReturn;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new BO.ObjectNotExistException("Order does not exist, please try again!", ex);
        }
   
    }

    private List<BO.OrderItem?> BuildItemsList(int id)//return a list of all the orderItems that related to a one order
    {
        var listReturn = from doi in Dal?.OrderItem.ReadAll()
                         where (doi?.OrderID == id)
                         select (new BO.OrderItem
                         {
                             ID = (int)doi?.OrderID!,
                             Amount = (int)doi?.Amount!,
                             Name = Dal?.Product.Read(doi?.ProductID ?? 0).Name,
                             Price = (double)doi?.Price!,
                             ProductID = (int)doi?.ProductID!,
                             TotalPrice = (double)doi?.Price! * (int)doi?.Amount!
                         });

        return listReturn.ToList();
    }
    private void CalcAmountAndPrice(ref int countAmountOfItems, ref double price, int? id)//search for all the products for the same order and return the price and the sum amount
    {
        foreach (DO.OrderItem? orderItem in Dal?.OrderItem.ReadAll()!)
        {
            if (orderItem?.OrderID == id)
            {
                countAmountOfItems += orderItem?.Amount ?? 0;
                price += (orderItem?.Price ?? 0) * (orderItem?.Amount ?? 0);
            }
        }
    }
}
