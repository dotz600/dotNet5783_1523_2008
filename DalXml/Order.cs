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
    public int Create(DO.Order obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        XElement orderXElement = serchXOrderInRoot(obj.ID, root);

        if (orderXElement != null)
            throw new ObjExistException("Order item allready found");
        //create xelemnt order
        XElement xmlObj = buildXElementOrder(obj);

        root?.Add(xmlObj);
        XmlTools.saveList(root, XmlTools.ordersPath);
        return obj.ID;
    }

    public void Delete(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);
        
        XElement orderXElement = serchXOrderInRoot(id, root);

        if (orderXElement == null)
            throw new ObjNotFoundException("Order doesn't found");
        
        orderXElement.Remove();
        XmlTools.saveList(root, XmlTools.ordersPath);
    }

    public DO.Order Read(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        DO.Order? order = (from x in root?.Elements()
                            where int.Parse(x?.Element("ID").Value) == id
                            select fromXElementToOrder(x)
                            ).FirstOrDefault();
        if (order != null)
            return (DO.Order)order;

        throw new ObjNotFoundException("Order doesn't found");
    }

    public IEnumerable<DO.Order?> ReadAll(Func<DO.Order?, bool>? predicate = null)
    { 
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        IEnumerable<DO.Order?> res;
        if (predicate == null)
            predicate = (order => true);

        res = (from x in root?.Elements()
               let order = fromXElementToOrder(x)
               where predicate((DO.Order?)order)
               select (DO.Order?)order);

        return res;

    }

    public DO.Order ReadIf(Func<DO.Order?, bool> predicate)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.ordersPath);

        DO.Order? order = (from x in root?.Elements()
                            let tmp = fromXElementToOrder(x)
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

        XElement XeOrder = serchXOrderInRoot(obj.ID, root);
        if (XeOrder == null)
            throw new ObjNotFoundException("cant update order");

        XeOrder.Element("CustomerName").Value = obj.CustomerName ;
        XeOrder.Element("CustomerEmail").Value = obj.CustomerEmail;
        XeOrder.Element("CustomerAdress").Value = obj.CustomerAdress;
        XeOrder.Element("OrderDate").Value = obj.OrderDate.ToString();
        XeOrder.Element("ShipDate").Value = obj.ShipDate.ToString();
        XeOrder.Element("DeliveryDate").Value = obj.DeliveryDate.ToString();

        XmlTools.saveList(root, XmlTools.ordersPath);
    }

    //private help functions
    private XElement buildXElementOrder(DO.Order obj)
    {
        return new XElement("Order", new XElement("ID", obj.ID),
                                                           new XElement("CustomerName", obj.CustomerName),
                                                           new XElement("CustomerEmail", obj.CustomerEmail),
                                                           new XElement("CustomerAdress", obj.CustomerAdress),
                                                           new XElement("OrderDate", obj.OrderDate),
                                                           new XElement("ShipDate", obj.ShipDate),
                                                           new XElement("DeliveryDate", obj.DeliveryDate));
    }
    private XElement serchXOrderInRoot(int id, XElement root)
    {
         return (from x in root?.Elements()
            where int.Parse(x?.Element("ID").Value) == id
            select x).FirstOrDefault();
    }
    private DO.Order? fromXElementToOrder(XElement obj)
    {
        var res = new DO.Order
        {
            ID = int.Parse(obj.Element("ID").Value),
            CustomerName = obj?.Element("CustomerName").Value,
            CustomerEmail = obj?.Element("CustomerEmail").Value,
            CustomerAdress = obj?.Element("CustomerAdress").Value,
            OrderDate = DateTime.Parse(obj?.Element("OrderDate").Value),
            ShipDate = obj?.Element("ShipDate").IsEmpty == false ? DateTime.Parse(obj?.Element("ShipDate").Value) : null,
            DeliveryDate = obj?.Element("DeliveryDate").IsEmpty == false ? DateTime.Parse(obj?.Element("DeliveryDate").Value) : null
        };

       return res;
    }
}
    