using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BIApi;


namespace BIImplementation;

internal class Order : IOrder
{

    private DalApi.IDal dal => new Dal.DalList();

    IEnumerable<BO.Order> IOrder.ReadAll()
    {
        throw new NotImplementedException();
    }

    BO.Order IOrder.Read(int orderId)
    {
        throw new NotImplementedException();
    }

    public void Create(BO.Order o)
    {
        throw new NotImplementedException();
    }

    void IOrder.Delete(int id)
    {
        throw new NotImplementedException();
    }

    BO.Order IOrder.UpdateDelivery(int orderId)
    {
        throw new NotImplementedException();
    }

    BO.OrderTracking IOrder.TrackingOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    void IOrder.UpdateShipping(int orderId)
    {
        throw new NotImplementedException();
    }
}
