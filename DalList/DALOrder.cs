﻿using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

internal class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order o1)//ovveride the exist obj with the new one
    {
        int t = DataSource.s_ordersArr.FindIndex(o => o?.ID == o1.ID);
        if (t != -1)
            DataSource.s_ordersArr[t] = o1;
        else
            throw new ObjNotFoundException("Order not exist");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Create(Order o1)//add the new obj to the array, return order ID
    {
        var y = DataSource.s_ordersArr.Find(x => x?.ID == o1.ID);//search if the obj allready in data base
        if (y != null)
            throw new ObjExistException("Order allready found");

        o1.ID = DataSource.Config.getIdRunNum();
        DataSource.s_ordersArr.Add(o1);
        return o1.ID;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)//delete the obj from the array
    {
        var res = from x in DataSource.s_ordersArr
                  where x?.ID == id
                  select x;

        if (!res.Any())
            throw new ObjNotFoundException("Order doesn't found");

        DataSource.s_ordersArr?.Remove(res.First());
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Read(int id)//return the obj
    {

        var res = DataSource.s_ordersArr.Find(x => x?.ID == id);
        if (res == null)
            throw new ObjNotFoundException("Order doesn't found");

        return (Order)res;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? predicate = null)//return all the obj array
    {
        if (predicate == null)
            return DataSource.s_ordersArr.ToList();
        else
            return DataSource.s_ordersArr.FindAll(x => predicate(x));
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order ReadIf(Func<Order?, bool> predicate)
    {
        Order? order = DataSource.s_ordersArr.FindLast(x => predicate(x));
        if (order != null)
            return (Order)order;
        else
            throw new ObjNotFoundException();

    }

}
