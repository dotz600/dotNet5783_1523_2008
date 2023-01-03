using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

class XmlTools
{
    #region Paths
    public static string productsPath = @"..\xml\ProductsXml.xml";//XElement
    public static string ordersPath = @"..\xml\OrdersXml.xml";//XMLSerializer
    public static string orderItemsPath = @"..\xml\OrderItemsXml.xml";//XMLSerializer
    #endregion

    #region save&load XML Functions
    public static XElement LoadListFromXMLElement(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return XElement.Load(filePath);
            }
            else
            {
                XElement rootElem = new XElement(filePath);
                rootElem.Save(filePath);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to load xml file: {filePath}", ex);
        }
    }

    public static void saveList(XElement xElement, string filePath)
    {
        try { xElement.Save(filePath); }
        catch(Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to save xml file: {filePath}", ex);
        }
    }
    #endregion

    #region search&build Functions

    public static DO.Product BuildProductDO(XElement newProduct)
    {
        return new DO.Product()
        {
            ID = int.Parse(newProduct.Element("ID")!.Value)
           ,
            Category = (Categories)Enum.Parse(typeof(Categories), newProduct.Element("Category").Value)
           ,
            InStock = int.Parse(newProduct.Element("InStock")!.Value)
           ,
            Name = newProduct.Element("Name").Value
           ,
            Price = double.Parse(newProduct.Element("Price").Value)
        };
    }


    public static XElement BuildProductXElement(DO.Product obj)
    {
        return new XElement("Product", new XElement("ID", obj.ID)
                                                            , new XElement("Name", obj.Name)
                                                            , new XElement("Price", obj.Price)
                                                            , new XElement("Category", obj.Category)
                                                            , new XElement("InStock", obj.InStock));
    }

    public static XElement SearchInRoot(int id, XElement productsRootElement)
    {
        return (from x in productsRootElement?.Elements()
                where int.Parse(x.Element("ID").Value) == id
                select x)?.FirstOrDefault();
    }

    #endregion
}

