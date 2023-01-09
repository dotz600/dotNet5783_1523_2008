
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
namespace Dal;
/// <summary>
/// XElement
/// </summary>
public class Product : IProduct
{
    public int Create(DO.Product obj)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        XElement checkIfExist = XmlTools.SearchInRoot(obj.ID, productsRootElement);

        if (checkIfExist != null)
            throw new ObjExistException("product ID exist!");

        XElement addProduct = XmlTools.BuildProductXElement(obj);

        productsRootElement?.Add(addProduct);
        XmlTools.saveList(productsRootElement, XmlTools.productsPath);
        return obj.ID;
    }

    
    public void Delete(int id)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);
        XElement checkIfExist = XmlTools.SearchInRoot(id, productsRootElement);

        if (checkIfExist == null)
            throw new ObjNotFoundException("Product doesn't found");

        checkIfExist.Remove();
        XmlTools.saveList(productsRootElement, XmlTools.productsPath);
    }

    

    public DO.Product Read(int id)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        XElement checkIfExist = XmlTools.SearchInRoot(id, productsRootElement);


        if (checkIfExist == null)
            throw new ObjNotFoundException("Product doesn't found");

        return XmlTools.BuildProductDO(checkIfExist);
    }

 

    public IEnumerable<DO.Product?> ReadAll(Func<DO.Product?, bool>? predicate = null)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        if (predicate != null)
        {
            return from x in productsRootElement?.Elements()
                   where x != null
                   select (DO.Product?)XmlTools.BuildProductDO(x);
        }

        return from x in productsRootElement?.Elements()
               where x!=null && predicate(XmlTools.BuildProductDO(x))
               select (DO.Product?)XmlTools.BuildProductDO(x);
 
    }

    public DO.Product ReadIf(Func<DO.Product?, bool> predicate)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        var p = from x in productsRootElement?.Elements()
                where x != null && predicate(XmlTools.BuildProductDO(x))
                select XmlTools.BuildProductDO(x);
        if (p == null)
            throw new ObjNotFoundException("Product doesn't found");
        return p.ElementAt(0);
    }

    public void Update(DO.Product obj)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        XElement checkIfExist = XmlTools.SearchInRoot(obj.ID, productsRootElement);

        if (checkIfExist != null)
        {
            checkIfExist.Remove();
            productsRootElement?.Add(XmlTools.BuildProductXElement(obj));
            XmlTools.saveList(productsRootElement, XmlTools.productsPath);
        }
        else // doesn't exist, checkIfExist is null here
            throw new ObjNotFoundException("Product not found");
    }
}
