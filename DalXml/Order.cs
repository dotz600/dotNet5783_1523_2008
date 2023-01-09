namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


/// <summary>
///XmlSerializer
/// </summary>
internal class Order : IOrder
{
    private int ID;//for new orders 
    private int GetIdRunNumber() { return ++ID; }
    public Order()//cnstr
    {
        //take all order from xml and search the max orderId, all the new order will get ID
        //from this number fowerd 
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);
        var tmp = from order in root.Elements()
                  select FromXElementToOrder(order);
        ID = tmp.MaxBy(x => x!.Value.ID)!.Value.ID;
    }
    public int Create(DO.Order obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        XElement orderXElement = SerchXOrderInRoot(obj.ID, root);

        if (orderXElement != null)
            throw new ObjExistException("Order allready found");
        //create xelemnt order
        obj.ID = GetIdRunNumber();
        XElement xmlObj = BuildXElementOrder(obj);

        root?.Add(xmlObj);
        XmlTools.SaveList(root!, XmlTools.ordersPath);
        return obj.ID;
    }

    public void Delete(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        XElement orderXElement = SerchXOrderInRoot(id, root);

        if (orderXElement == null)
            throw new ObjNotFoundException("Order doesn't found");

        orderXElement.Remove();
        XmlTools.SaveList(root, XmlTools.ordersPath);
    }

    public DO.Order Read(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        DO.Order? order = (from x in root?.Elements()
                           where int.Parse(x?.Element("ID")?.Value!) == id
                           select FromXElementToOrder(x)
                            ).FirstOrDefault();
        if (order != null)
            return (DO.Order)order;

        throw new ObjNotFoundException("Order doesn't found");
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? predicate = null)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        IEnumerable<DO.Order?> res;
        predicate ??= (order => true);

        res = (from x in root?.Elements()
               let order = FromXElementToOrder(x)
               where predicate((DO.Order?)order)
               select (DO.Order?)order);

        return res;

    }

    public DO.Order ReadIf(Func<DO.Order?, bool> predicate)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        DO.Order? order = (from x in root?.Elements()
                           let tmp = FromXElementToOrder(x)
                           where predicate(tmp)
                           select tmp
                            ).FirstOrDefault();
        if (order != null)
            return (DO.Order)order;

        throw new ObjNotFoundException("Order doesn't found");
    }

    public void Update(DO.Order obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        XElement XeOrder = SerchXOrderInRoot(obj.ID, root);
        if (XeOrder == null)
            throw new ObjNotFoundException("cant update order");

        XeOrder.Element("CustomerName")!.Value = obj.CustomerName!;
        XeOrder.Element("CustomerEmail")!.Value = obj.CustomerEmail!;
        XeOrder.Element("CustomerAdress")!.Value = obj.CustomerAdress!;
        XeOrder.Element("OrderDate")!.Value = obj.OrderDate.ToString()!;
        if (obj.ShipDate != null)
            XeOrder.Element("ShipDate")!.Value = obj.ShipDate.ToString()!;
        if (obj.DeliveryDate != null)
            XeOrder.Element("DeliveryDate")!.Value = obj.DeliveryDate.ToString()!;


        XmlTools.SaveList(root, XmlTools.ordersPath);
    }

    //private help functions
    private static XElement BuildXElementOrder(DO.Order obj)
    {
        return new XElement("Order", new XElement("ID", obj.ID),
                                                           new XElement("CustomerName", obj.CustomerName),
                                                           new XElement("CustomerEmail", obj.CustomerEmail),
                                                           new XElement("CustomerAdress", obj.CustomerAdress),
                                                           new XElement("OrderDate", obj.OrderDate),
                                                           new XElement("ShipDate", obj.ShipDate),
                                                           new XElement("DeliveryDate", obj.DeliveryDate));
    }
    private static XElement SerchXOrderInRoot(int id, XElement root)
    {
        return (from x in root?.Elements()
                where int.Parse(x?.Element("ID")?.Value!) == id
                select x).FirstOrDefault()!;
    }
    private static DO.Order? FromXElementToOrder(XElement obj)
    {
        var res = new DO.Order
        {
            ID = int.Parse(obj?.Element("ID")?.Value!),
            CustomerName = obj?.Element("CustomerName")?.Value!,
            CustomerEmail = obj?.Element("CustomerEmail")?.Value!,
            CustomerAdress = obj?.Element("CustomerAdress")?.Value!,
            OrderDate = DateTime.Parse(obj?.Element("OrderDate")?.Value!),
            ShipDate = obj?.Element("ShipDate")?.IsEmpty == false ? DateTime.Parse(obj?.Element("ShipDate")?.Value!) : null,
            DeliveryDate = obj?.Element("DeliveryDate")?.IsEmpty == false ? DateTime.Parse(obj?.Element("DeliveryDate")?.Value!) : null
        };
        return res;
    }



}
    