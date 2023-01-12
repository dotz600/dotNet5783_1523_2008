namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;


/// <summary>
///XmlSerializer
/// </summary>
/// 
internal class Order : IOrder
{
    private int ID;//for new orders 
    private int GetIdRunNumber() { return ++ID; }
    public Order()//cnstr
    {
        //take all order from xml and search the max orderId, all the new order will get ID
        //from this number fowerd 
        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);
        var tmp = from order in orders
                  select order;
        ID = tmp.MaxBy(x => x!.Value.ID)!.Value.ID;
    }

    public int Create(DO.Order obj)
    {

        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        var y = orders.Find(x => x?.ID == obj.ID);//search if the obj allready in data base
        if (y != null)
            throw new ObjExistException("Order allready found");

        obj.ID = GetIdRunNumber();

        orders.Add(obj);

        SaveListToXMLSerializer(orders, XmlTools.ordersPath);

        return obj.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        var y = orders.Find(x => x?.ID == id);//search if the obj allready in data base
        if (y is null)
            throw new ObjNotFoundException("Order not found");

        orders.Remove(y);

        SaveListToXMLSerializer(orders, XmlTools.ordersPath);

    }

    public DO.Order Read(int id)
    {

        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        var y = orders.Find(x => x?.ID == id);//search if the obj allready in data base
        if (y is null)
            throw new ObjNotFoundException("Order not found");
        return (DO.Order)y!;
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? predicate = null)
    {
        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        IEnumerable<DO.Order?> res;

        predicate ??= (order => true);

        res = (from order in orders
               where predicate((DO.Order?)order)
               select (DO.Order?)order);

        return res;

    }

    public DO.Order ReadIf(Func<DO.Order?, bool> predicate)
    {
        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        DO.Order? order = (from x in orders
                           where predicate(x)
                           select x
                            ).FirstOrDefault();
        if (order != null)
            return (DO.Order)order;

        throw new ObjNotFoundException("Order doesn't found");
    }

    public void Update(DO.Order obj)
    {
        List<DO.Order?> orders = LoadListFromXMLSerializer<DO.Order?>(XmlTools.ordersPath);

        var y = orders.FindIndex(x => x?.ID == obj.ID);//search if the obj allready in data base
        if (y == -1)
            throw new ObjNotFoundException("Order not exist");

        orders[y] = obj;

        SaveListToXMLSerializer(orders, XmlTools.ordersPath);

    }

    //private help functions



    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to create xml file: {filePath}", ex);
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to load xml file: {filePath}", ex);
        }
    }


}
