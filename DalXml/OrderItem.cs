namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// XmlSerializer
/// </summary>
internal class OrderItem : IOrderItem
{
    public int Create(DO.OrderItem obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        XElement ot = searchOrderItemInRoot(obj);

        if (ot != null)
            throw new ObjExistException("Order item allready found");

        XElement xmlObj = buildOrderItemXElemnt();

        root.Add(xmlObj);
        XmlTools.saveList(root, XmlTools.orderItemsPath);
        return obj.OrderID;
    }

    public void Delete(int OrderId)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        XElement ot = (from x in root?.Elements()
                       where int.Parse(x?.Element("OrderID").Value) == OrderId
                       select x).FirstOrDefault();

        if (ot == null)
            throw new ObjNotFoundException("Order item doesn't found");
        ot.Remove();
        XmlTools.saveList(root, XmlTools.orderItemsPath);

    }

    public DO.OrderItem Read(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        //search and convert
        DO.OrderItem? ot = (from x in root?.Elements()
                            where int.Parse(x?.Element("OrderID").Value) == id
                            select buildOrderItemDO(x)
                            ).FirstOrDefault();
        if (ot != null)
            return (DO.OrderItem)ot;

        throw new ObjNotFoundException("Order item doesn't found");
    }

    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? predicate = null)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        IEnumerable<DO.OrderItem?> res;
        if (predicate == null)
            predicate = (ot => true);

        res = (from x in root?.Elements()
               let tmp = buildOrderItemDO(x)
               where predicate((DO.OrderItem?)tmp)
               select (DO.OrderItem?)tmp);

        return res;
    }

    public DO.OrderItem ReadIf(Func<DO.OrderItem?, bool> predicate)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        
        DO.OrderItem? ot = (from x in root?.Elements()
                            let tmp = buildOrderItemDO(x)
                            where predicate(tmp)
                            select tmp
                            ).FirstOrDefault();
        if (ot != null)
            return (DO.OrderItem)ot;

        throw new ObjNotFoundException("Order item doesn't found");
    }

    public DO.OrderItem ReadProductId(int productId)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        //search and convert
        DO.OrderItem? ot = (from x in root?.Elements()
                            where int.Parse(x?.Element("ProductID").Value) == ProductId
                            select buildOrderItemDO(x)
                            ).FirstOrDefault();
        if (ot != null)
            return (DO.OrderItem)ot;

        throw new ObjNotFoundException("Order item doesn't found");
    }

    public void Update(DO.OrderItem obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        
        XElement ot = (from x in root?.Elements()
                            where int.Parse(x?.Element("OrderID").Value) == obj.OrderID
                            select x).FirstOrDefault();
        if(ot == null)
            throw new ObjNotFoundException("cant update order item");
        
        ot.Element("ProductID").Value = obj.ProductID;
        ot.Element("Price").Value = obj.Price;
        ot.Element("Amount").Value = obj.Amount;

        XmlTools.saveList(root, XmlTools.orderItemsPath);
    }

    /// <summary>
    /// private help functions
    /// </summary>
    /// <returns></returns>
    private XElement buildOrderItemXElemnt(DO.OrderItem obj)//buils eml order item obj
    {
        return new XElement("OrderItem", new XElement("ProductID", obj.ProductID),
                                                           new XElement("OrderID", obj.OrderID),
                                                           new XElement("Price", obj.Price),
                                                           new XElement("Amount", obj.Amount));
    }
    private XElement searchOrderItemInRoot(DO.OrderItem obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        return (from x in root?.Elements()
                where int.Parse(x?.Element("ProductID").Value) == obj.ProductID
                && int.Parse(x?.Element("OrderID").Value) == obj.OrderID
                select x).FirstOrDefault();
    }
    private DO.OrderItem? buildOrderItemDO(XElement obj)
    {
       
        return new DO.OrderItem
        {
            Amount = int.Parse(obj.Element("Amount").Value),
            OrderID = int.Parse(obj?.Element("OrderID").Value),
            Price = int.Parse(obj?.Element("Price").Value),
            ProductID = int.Parse(obj?.Element("ProductID").Value)
        };
    }

}
