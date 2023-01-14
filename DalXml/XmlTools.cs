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
                XElement rootElem = new(filePath);
                rootElem.Save(filePath);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to load xml file: {filePath}", ex);
        }
    }

    public static void SaveList(XElement xElement, string filePath)
    {
        try { xElement.Save(filePath); }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to save xml file: {filePath}", ex);
        }
    }
    #endregion

}

