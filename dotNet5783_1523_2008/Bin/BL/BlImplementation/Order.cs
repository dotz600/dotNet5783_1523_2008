using BlApi;
using BO;
using System.Data;

namespace BlImplementation;

internal class Order : IOrder
{

    private DalApi.IDal dal => new Dal.DalList();

    public IEnumerable<BO.OrderForList> ReadAll()//returns list of BO.Order
    {
        List<BO.OrderForList> list = new List<BO.OrderForList>();//create list to return

        foreach (DO.Order order in dal.Order.ReadAll())//build and push elements to the list
        {
            int countAmountOfItems = 0;
            double sumPrice = 0;

            ///maybe we need to check if all the amount in order item is exist in stock and only after that order is confirm
            /// the problem is that, the truth is all the order we have in data base should be confirmed,  
            
            OrderStatus status = OrderStatus.ConfirmedOrder;//every order in the data base allready confirm

            if (order.ShipDate < DateTime.Now && order.ShipDate != DateTime.MinValue)//status update(provide or sent)
                status = OrderStatus.Sent;
            if (order.DeliveryDate < DateTime.Now && order.DeliveryDate != DateTime.MinValue)
                status = OrderStatus.Provided;

            calcAmountAndPrice(ref countAmountOfItems, ref sumPrice, order.ID);

            BO.OrderForList o = new BO.OrderForList() //build single element
            {
                ID = order.ID,
                AmountOfItems = countAmountOfItems,
                CustomerName = order.CustomerName,
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
                DO.Order o;
                o = dal.Order.Read(orderId);
                BO.Order orderReturn = new BO.Order()//build order to return
                {
                    ID = o.ID,
                    CustomerAdress = o.CustomerAdress,
                    CustomerName = o.CustomerName,
                    CustomerEmail = o.CustomerEmail,
                    ShipDate = o.ShipDate,
                    PaymentDate = o.OrderDate,
                    DeliveryDate = o.DeliveryDate,
                    OrderDate = o.OrderDate,

                };

                orderReturn.Items = buildItemsList(orderId);
                //calc price and amount
                int amount = 0;
                double price = 0;
                calcAmountAndPrice(ref amount, ref price, o.ID);
                //update the results
                orderReturn.TotalPrice = price;
                OrderStatus status = OrderStatus.ConfirmedOrder;//evrey order in the data base allready confirm
                if (orderReturn.ShipDate < DateTime.Now && orderReturn.ShipDate != DateTime.MinValue)
                    status = OrderStatus.Sent;
                if (orderReturn.DeliveryDate < DateTime.Now && orderReturn.DeliveryDate != DateTime.MinValue)
                    status = OrderStatus.Provided;
                orderReturn.Status = status;

                return orderReturn;
            }//check if the order exist in DL
            catch (DalApi.ObjNotFoundException ex)
            {
                throw new ObjectNotExistException("BO.Order.Read", ex);
            }
        }
        else
            throw new NegativeIDException("invalid input");
    }
    public BO.Order UpdateDelivery(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
            if (order.ShipDate == DateTime.MinValue)
                throw new UpdateObjectFailedException("Send order before!");
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);

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
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException("BO.Order.UpdateDelivery", ex);
        }

    }
    public BO.Order UpdateShipping(int orderId)//update status, and returns BO.Order
    {
        try
        {
            DO.Order order = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
            if (order.DeliveryDate > order.ShipDate)
                throw new UpdateObjectFailedException("Order was provided!");
            order.ShipDate = DateTime.Now;
            dal.Order.Update(order);//update the order in data source
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
            int temp = 0;
            double price = 0;
            calcAmountAndPrice(ref temp, ref price, orderReturn.ID);//update the total price
            orderReturn.TotalPrice = price;
            return orderReturn;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException("BO.Order.UpdateShipping", ex);
        }
        catch (UpdateObjectFailedException ex)
        {
            throw ex;
        }
    }
    public BO.OrderTracking TrackingOrder(int orderId)//returns current status, and list of events that were occurred in these order
    {
        try
        {
            DO.Order o = dal.Order.Read(orderId);//if doesn't exist, throw from DALOrder
            OrderStatus orderStatus = OrderStatus.ConfirmedOrder;

            if (o.ShipDate < DateTime.Now && o.ShipDate != DateTime.MinValue)//if true - the order has been sent and need to update orderStatus
                orderStatus = OrderStatus.Sent;
            if (o.DeliveryDate < DateTime.Now && o.DeliveryDate != DateTime.MinValue)
                orderStatus = OrderStatus.Provided;

            BO.OrderTracking orderTrackingToReturn = new BO.OrderTracking { ID = orderId, Status = orderStatus };
          
            BO.OrderTracking.DateAndStatus dateAndStatus1, dateAndStatus2 , dateAndStatus3;

            dateAndStatus1.dt = o.OrderDate;
            dateAndStatus1.os = OrderStatus.ConfirmedOrder;

            if (orderTrackingToReturn.Events == null)
                orderTrackingToReturn.Events = new List<BO.OrderTracking.DateAndStatus>();

            orderTrackingToReturn.Events.Add(dateAndStatus1);

            if (o.ShipDate < DateTime.Now && o.ShipDate != DateTime.MinValue)
            {
                dateAndStatus2.dt = o.ShipDate;
                dateAndStatus2.os = OrderStatus.Sent;
                orderTrackingToReturn.Events.Add(dateAndStatus2);
            }
            if (o.DeliveryDate < DateTime.Now && o.DeliveryDate != DateTime.MinValue)
            {
                dateAndStatus3.dt = o.DeliveryDate;
                dateAndStatus3.os = OrderStatus.Provided;
                orderTrackingToReturn.Events.Add(dateAndStatus3);
            }
            
            return orderTrackingToReturn;
        }
        catch (DalApi.ObjNotFoundException ex)
        {
            throw new ObjectNotExistException("BO.Order.TrackingOrder", ex);
        }
    

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
                    ID = doi.OrderID,
                    Amount = doi.Amount,
                    Name = dal.Product.Read(doi.ProductID).Name,
                    Price = doi.Price,
                    ProductID = doi.ProductID,
                    TotalPrice = doi.Price * doi.Amount
                };
                listReturn.Add(boi);
            }
        }
        return listReturn;
    }
    public void calcAmountAndPrice(ref int countAmountOfItems, ref double price, int id)
    {
        foreach (DO.OrderItem orderItem in dal.OrderItem.ReadAll())
        {
            if (orderItem.OrderID == id)
            {
                countAmountOfItems += orderItem.Amount;
                price += orderItem.Price * orderItem.Amount;
            }
        }
    }
}
