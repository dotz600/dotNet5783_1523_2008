namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

/// <summary>
///XmlSerializer
/// </summary>
internal class Order : IOrder
{
    public int Create(DO.Order obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order ReadIf(Func<DO.Order?, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order obj)
    {
        throw new NotImplementedException();
    }
}
