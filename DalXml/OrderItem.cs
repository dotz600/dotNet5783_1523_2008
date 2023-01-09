namespace Dal;
using DalApi;
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

        XElement ot = SearchOrderItemInRoot(obj);

        if (ot != null)
            throw new ObjExistException("Order item allready found");

        XElement xmlObj = BuildOrderItemXElemnt(obj);

        root.Add(xmlObj);
        XmlTools.SaveList(root, XmlTools.orderItemsPath);
        return obj.OrderID;
    }

    public void Delete(int OrderId)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        XElement ot = (from x in root?.Elements()!
                       where int.Parse(x?.Element("OrderID")?.Value!) == OrderId
                       select x).FirstOrDefault()!;

        if (ot == null)
            throw new ObjNotFoundException("Order item doesn't found");
        ot.Remove();
        XmlTools.SaveList(root!, XmlTools.orderItemsPath);

    }

    public DO.OrderItem Read(int id)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        //search and convert
        DO.OrderItem? ot = (from x in root?.Elements()
                            where int.Parse(x?.Element("OrderID")?.Value!) == id
                            select BuildOrderItemDO(x)
                            ).FirstOrDefault();
        if (ot != null)
            return (DO.OrderItem)ot;

        throw new ObjNotFoundException("Order item doesn't found");
    }

    public IEnumerable<DO.OrderItem?> ReadAll(Func<DO.OrderItem?, bool>? predicate = null)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        predicate ??= (ot => true);

        IEnumerable<DO.OrderItem?> res = (from x in root?.Elements()
                                          let tmp = BuildOrderItemDO(x)
                                          where predicate((DO.OrderItem?)tmp)
                                          select (DO.OrderItem?)tmp);

        return res;
    }

    public DO.OrderItem ReadIf(Func<DO.OrderItem?, bool> predicate)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        DO.OrderItem? ot = (from x in root?.Elements()
                            let tmp = BuildOrderItemDO(x)
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
                            where int.Parse(x?.Element("ProductID")!.Value!) == productId
                            select BuildOrderItemDO(x)
                            ).FirstOrDefault();
        if (ot != null)
            return (DO.OrderItem)ot;

        throw new ObjNotFoundException("Order item doesn't found");
    }

    public void Update(DO.OrderItem obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);
        //int res;
        XElement ot = (from x in root?.Elements()
                       where int.Parse(x?.Element("OrderID")!.Value!) == obj.OrderID
                       select x).FirstOrDefault()!;
        if (ot == null)
            throw new ObjNotFoundException("cant update order item");

        ot.Element("ProductID")!.Value = obj.ProductID.ToString();
        ot.Element("Price")!.Value = obj.Price.ToString();
        ot.Element("Amount")!.Value = obj.Amount.ToString();

        XmlTools.SaveList(root!, XmlTools.orderItemsPath);
    }

    /// <summary>
    /// private help functions
    /// </summary>
    /// <returns></returns>
    private static XElement BuildOrderItemXElemnt(DO.OrderItem obj)//buils xml order item obj
    {
        return new XElement("OrderItem", new XElement("ProductID", obj.ProductID),
                                                           new XElement("OrderID", obj.OrderID),
                                                           new XElement("Price", obj.Price),
                                                           new XElement("Amount", obj.Amount));
    }
    private static XElement SearchOrderItemInRoot(DO.OrderItem obj)
    {
        XElement root = XmlTools.LoadListFromXMLElement(XmlTools.orderItemsPath);

        return (from x in root?.Elements()!
                where int.Parse(x?.Element("ProductID")?.Value!) == obj.ProductID
                && int.Parse(x?.Element("OrderID")?.Value!) == obj.OrderID
                select x).FirstOrDefault()!;
    }
    private static DO.OrderItem? BuildOrderItemDO(XElement obj)
    {

        return new DO.OrderItem
        {
            Amount = int.Parse(obj?.Element("Amount")?.Value!),
            OrderID = int.Parse(obj?.Element("OrderID")?.Value!),
            Price = double.Parse(obj?.Element("Price")?.Value!),
            ProductID = int.Parse(obj?.Element("ProductID")?.Value!)
        };
    }

}
