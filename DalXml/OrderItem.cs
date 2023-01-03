namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

/// <summary>
/// XmlSerializer
/// </summary>
internal class OrderItem : IOrderItem
{
    public int Create(DO.OrderItem obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem ReadIf(Func<DO.OrderItem?, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem ReadProductId(int productId)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem obj)
    {
        throw new NotImplementedException();
    }
}
