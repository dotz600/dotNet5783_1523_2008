
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        XElement checkIfExist = SearchInRoot(obj.ID, productsRootElement);

        if (checkIfExist != null)
            throw new ObjExistException("product ID exist!");

        XElement addProduct = BuildProductXElement(obj);

        productsRootElement?.Add(addProduct);
        XmlTools.SaveList(productsRootElement!, XmlTools.productsPath);

        return obj.ID;
    }

    
    public void Delete(int id)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);
        XElement checkIfExist = SearchInRoot(id, productsRootElement);

        if (checkIfExist == null)
            throw new ObjNotFoundException("Product doesn't found");

        checkIfExist.Remove();
        XmlTools.SaveList(productsRootElement, XmlTools.productsPath);
    }

    

    public DO.Product Read(int id)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        XElement checkIfExist = SearchInRoot(id, productsRootElement);


        if (checkIfExist == null)
            throw new ObjNotFoundException("Product doesn't found");

        return BuildProductDO(checkIfExist);
    }

 

    public IEnumerable<DO.Product?> ReadAll(Func<DO.Product?, bool>? predicate = null)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        if (predicate == null)
        {
            return from x in productsRootElement?.Elements()
                   where x != null
                   select (DO.Product?)BuildProductDO(x);
        }

        return from x in productsRootElement?.Elements()
               where x!=null && predicate(BuildProductDO(x))
               select (DO.Product?)BuildProductDO(x);
 
    }

    public DO.Product ReadIf(Func<DO.Product?, bool> predicate)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        var p = from x in productsRootElement?.Elements()
                where x != null && predicate(BuildProductDO(x))
                select BuildProductDO(x);
        if (p == null)
            throw new ObjNotFoundException("Product doesn't found");
        return p.ElementAt(0);
    }

    public void Update(DO.Product obj)
    {
        XElement productsRootElement = XmlTools.LoadListFromXMLElement(XmlTools.productsPath);

        XElement checkIfExist = SearchInRoot(obj.ID, productsRootElement);

        if (checkIfExist != null)
        {
            checkIfExist.Element("Category")!.Value = obj.Category.ToString();
            checkIfExist.Element("InStock")!.Value = obj.InStock.ToString();
            checkIfExist.Element("Name")!.Value = obj.Name!;
            checkIfExist.Element("Price")!.Value = obj.Price.ToString();

            XmlTools.SaveList(productsRootElement!, XmlTools.productsPath);
        }
        else // doesn't exist, checkIfExist is null here
            throw new ObjNotFoundException("Product not found");
    }

    private static DO.Product BuildProductDO(XElement newProduct)
    {
        return new DO.Product()
        {
            ID = int.Parse(newProduct.Element("ID")!.Value!)
           ,
            Category = (Categories)Enum.Parse(typeof(Categories), newProduct?.Element("Category")!.Value!)
           ,
            InStock = int.Parse(newProduct?.Element("InStock")?.Value!)
           ,
            Name = newProduct?.Element("Name")?.Value!
           ,
            Price = double.Parse(newProduct?.Element("Price")?.Value!)
        };
    }


    private static XElement BuildProductXElement(DO.Product obj)
    {
        return new XElement("Product", new XElement("ID", obj.ID)
                                                            , new XElement("Name", obj.Name)
                                                            , new XElement("Price", obj.Price)
                                                            , new XElement("Category", obj.Category)
                                                            , new XElement("InStock", obj.InStock));
    }

    private static XElement SearchInRoot(int id, XElement productsRootElement)
    {
        return (from x in productsRootElement?.Elements()!
                where int.Parse(x.Element("ID")!.Value!) == id
                select x).FirstOrDefault()!;
    }

}
