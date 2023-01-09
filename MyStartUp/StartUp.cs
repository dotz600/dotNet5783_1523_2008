﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using DalApi;
using DO;
namespace MyStartUp;

internal class StartUp
{
    //list for holding the entity
    internal static List<DO.Product?> s_productsArr = new();
    internal static List<DO.Order?> s_ordersArr = new();
    internal static List<DO.OrderItem?> s_ordersItemArr = new();

    internal static List<string> names = new();
    internal static List<string> lastName = new();
    internal static List<string> emails = new();
    internal static List<string> address = new();

    //path for the xml files
    public string productsPath = @"..\xml\ProductsXml.xml";//XElement
    public  string ordersPath = @"..\xml\OrdersXml.xml";//XMLSerializer
    public  string orderItemsPath = @"..\xml\OrderItemsXml.xml";//XMLSerializer

    internal static int s_idRunNum = 1000;//for order ID
    internal static int getIdRunNum()
    {
        return ++s_idRunNum;
    }
    static internal Random rand = new Random(DateTime.Now.Millisecond);

    private int getProductRandomID()//rec function that gunrate rundom product id,and check that random dosnt exist
    {
        int tmp = rand.Next(100000, 1000000);
        var isExist = (from x in s_productsArr
                       where x.Value.ID == tmp
                       select x).FirstOrDefault();
        if (isExist == null)
            return tmp;

        return getProductRandomID();

    }
    
    public StartUp()
    {
        //initeliz the list
        s_idRunNum = 1000;
        nameSturtup();
        lastNameSturtup();
        emailSturtup();
        addressSturtup();
        addProduct();//make 15 products

        for (int i = 0; i < 20; i++)//make 20 order
            addOrder(i);

        for (int i = 0; i < 40; i++)//make 40 order item
            addOrderItem(i);

        //put the date to xml file
        //save orders
        XElement orderRoot = LoadListFromXMLElement(ordersPath);
        for(int i = 0; i<  s_ordersArr.Count; i++)
        {
            orderRoot.Add(buildXElementOrder((DO.Order)s_ordersArr[i]!));
        }
        saveList(orderRoot, ordersPath);

        //save products
        XElement productRoot = LoadListFromXMLElement(productsPath);
        for (int i = 0; i < s_productsArr.Count; i++)
        {
            productRoot.Add(buildXElementProduct((DO.Product)s_productsArr[i]!));
        }
        saveList(productRoot, productsPath);

        //save order items
        XElement orderItemRoot = LoadListFromXMLElement(orderItemsPath);
        for(int i = 0;i < s_ordersItemArr.Count; i++)
        {
            orderItemRoot.Add(buildXElementOrderItem((DO.OrderItem)s_ordersItemArr[i]!));
        }
        saveList(orderItemRoot, orderItemsPath);
    }

    private static void emailSturtup()//adding name + @gmail
    {
        emails = new();
        foreach (var n in names)
            emails.Add(n + "@gmail.com");
    }

    private static void nameSturtup()
    {
        names.Add("Balthasar"); names.Add("Irving"); names.Add("Feivel"); names.Add("Judah"); names.Add("Ansel"); names.Add("Quang");
        names.Add("Hershel"); names.Add("Avrum"); names.Add("Gaspar"); names.Add("Ephraim"); names.Add("Herschel"); names.Add("Isaac"); names.Add("Hyman");
        names.Add("Mendel"); names.Add("Moss"); names.Add("Shraga"); names.Add("Nosson"); names.Add("Lazer"); names.Add("Jacob"); names.Add("Aaron");
    }

    private static void lastNameSturtup()
    {
        lastName.Add("Cohen"); lastName.Add("Anwar"); lastName.Add("Karim"); lastName.Add("Rashid"); lastName.Add("Adam");
        lastName.Add("Daniel"); lastName.Add("Abraham"); lastName.Add("Eden"); lastName.Add("Moran"); lastName.Add("Noach"); lastName.Add("Simon");
        lastName.Add("Khan"); lastName.Add("Reynolds"); lastName.Add("Reed"); lastName.Add("Osborne"); lastName.Add("Aguilar"); lastName.Add("Miller");
        lastName.Add("Arnold"); lastName.Add("Hutchinson"); lastName.Add("Morton"); lastName.Add("Grant");
    }

    private static void addressSturtup()
    {

        address.Add("Robin Close");address.Add("Partridge Close");address.Add("Queens Road");address.Add("Eastern Avenue");address.Add("Castle Lane");
        address.Add("Station Close");address.Add("The Street");address.Add("Teal Close");address.Add("Lancaster Avenue");address.Add("Byron Close");
        address.Add("Market Place");address.Add("Chestnut Grove");address.Add("Lodge Close");address.Add("Old Lane");address.Add("Thornhill Road");
        address.Add("Blind Lane");address.Add("Grove Road");address.Add("Court Road");address.Add("York Road");address.Add("Heron Close");
    }

    private void addProduct()//make new 18 product
    {
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Doughnut", Category = Categories.Bakery, InStock = 15, Price = 5 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Chicken", Category = Categories.Meat, InStock = 25, Price = 30 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Yellow cheese", Category = Categories.Deli, InStock = 12, Price = 33 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Bean", Category = Categories.Frozen, InStock = 7, Price = 16 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Mop", Category = Categories.Cleaning, InStock = 3, Price = 22 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Cows Milk", Category = Categories.Dairy, InStock = 45, Price = 4.99 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Gum", Category = Categories.Sweets, InStock = 75, Price = 0.99 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Makeup", Category = Categories.Beauty, InStock = 15, Price = 7 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Bread", Category = Categories.Bakery, InStock = 50, Price = 5.99 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Eggs", Category = Categories.Deli, InStock = 500, Price = 1.5 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Milki", Category = Categories.Deli, InStock = 30, Price = 4.7 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Baguette", Category = Categories.Bakery, InStock = 30, Price = 5.5 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Chocolate Croissant", Category = Categories.Bakery, InStock = 15, Price = 3 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Garbage Bags", Category = Categories.Cleaning, InStock = 50, Price = 10 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Bleach", Category = Categories.Cleaning, InStock = 15, Price = 32.5 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Salmon", Category = Categories.Meat, InStock = 15, Price = 105.24 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Ice Cream", Category = Categories.Frozen, InStock = 7, Price = 17.5 });
        s_productsArr.Add(new DO.Product { ID = getProductRandomID(), Name = "Disposable Cups", Category = Categories.Grocery, InStock = 200, Price = 7.5 });
    }

    private void addOrder(int i)//create 20 orders
    {
        DO.Order res = new();
        res.ID = getIdRunNum();
        res.CustomerName = names[i] + ' ' + lastName[i];
        res.CustomerEmail = emails[i];
        res.CustomerAdress = address[i];

        res.OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//time now - random time
        DateTime? tmp = res.OrderDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//make random temp time
        if (tmp > DateTime.Now)//if the random time is bigger than now can add the order and return
        {
            s_ordersArr.Add(res);
            return;
        }//else
        res.ShipDate = tmp;
        tmp = res.ShipDate + new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship date + random time

        if (tmp > DateTime.Now)//if true - the order has been sent but dosnt arrive yet. manger need to update delivery date
        {
            s_ordersArr.Add(res);
            return;
        }//else update delivery date
        res.DeliveryDate = tmp;
        
        s_ordersArr.Add(res);
    }

    private void addOrderItem(int i)//40 of those
    {
        int amount = rand.Next(1, 5);//amount of products in order
        int randomProduct = rand.Next(s_productsArr.Count);
        DO.OrderItem ot1 = new();
        
        ot1.ProductID = (int)s_productsArr[randomProduct]?.ID!;  //alreday added 10 product to the list 
        ot1.OrderID = (int)s_ordersArr[i % 20]?.ID!; // allready added 20 orders to the list
        ot1.Amount = amount;//amount is random number
        ot1.Price = (int)s_productsArr[randomProduct]?.Price! * amount;
        s_ordersItemArr.Add(ot1);
    }

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

    public XElement buildXElementProduct(DO.Product obj)
    {
        return new XElement("Product", new XElement("ID", obj.ID)
                                                            , new XElement("Name", obj.Name)
                                                            , new XElement("Price", obj.Price)
                                                            , new XElement("Category", obj.Category)
                                                            , new XElement("InStock", obj.InStock));
    }
    private XElement buildXElementOrderItem(DO.OrderItem obj)//buils xml orderItem obj
    {
        return new XElement("OrderItem", new XElement("ProductID", obj.ProductID),
                                                           new XElement("OrderID", obj.OrderID),
                                                           new XElement("Price", obj.Price),
                                                           new XElement("Amount", obj.Amount));
    }

    private XElement LoadListFromXMLElement(string filePath)
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
    private void saveList(XElement xElement, string filePath)
    {
        try { xElement.Save(filePath); }
        catch (Exception ex)
        {
            throw new XMLFileSaveLoadException($"fail to save xml file: {filePath}", ex);
        }
    }
}
