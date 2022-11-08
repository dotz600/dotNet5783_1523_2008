﻿
using DO;
using System.Xml.Linq;
using static Dal.DataSource;

namespace Dal;

internal static class DataSource
{
    static DataSource() { s_Initialize(); }
    
    static readonly int RandomNum = 100000;

    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] ordersItemArr = new OrderItem[200];

   
    private static void addOrder(Order o1)
    {
        ordersArr[Config.ordersSize++] = o1;
    }

    private static void addOrderItem(int index = 0)
    {
        for (int i = 0; i < 4 * index; i++)
        {
            ordersItemArr[Config.ordersItemSize].ProductID = productsArr[i % Config.productsSize].ID;
            ordersItemArr[Config.ordersItemSize].OrderID = ordersArr[i % Config.ordersSize].ID;
            ordersItemArr[Config.ordersItemSize].Amount = Config.rand.Next(5);
            ordersItemArr[Config.ordersItemSize].Price = productsArr[i].Price * ordersItemArr[i].Amount;
            Config.ordersItemSize++;
        }
    }

    private static void addProduct(Product p1)
    {
        productsArr[Config.productsSize++] = p1;
    }

    private static void s_Initialize()
    {
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000) ,Name = "doughnut", category = Categories.Bakery, InStock = 15  ,Price = 5} );
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "chicken", category = Categories.Meat, InStock = 25, Price = 30 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "Yellow cheese", category = Categories.Deli, InStock = 12, Price = 33 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bean", category = Categories.Frozen, InStock = 7, Price = 16 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mop", category = Categories.Cleaning, InStock = 3, Price = 22 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "cows milk", category = Categories.Dairy, InStock = 45, Price = 4.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "gum", category = Categories.Sweets, InStock = 75, Price = 0.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "makeup", category = Categories.Beauty, InStock = 15, Price = 7 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bread", category = Categories.Bakery, InStock = 50, Price = 5.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mince", category = Categories.Meat, InStock = 0, Price = 42.5 });



        //  TO DO --- adding 20 orders. 
        for (int i = 97; i < 117; i++)
        {
            addOrder(new Order
            {
                ID = Config.IdRunNum,
                CustomerName = "Avi" + (char)i,
                CustomerEmail = "Avi"+(char)i + "@gmail.com",
                CustomerAdress = "jerusalem" + (char)i,
                OrderDate = DateTime.MinValue,
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            });
        }
        //TO DO --- for loop 20% dates without dateTime
        for(int i = 0; i <Config.ordersSize; i++)
        {
            ordersArr[i].OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//nedd to check the range date its return 
            ordersArr[i].ShipDate = ordersArr[i].OrderDate + new TimeSpan(0, 0, Config.rand.Next(30));//ship date can be between 0 -30 from the order day 
            if(ordersArr[i].ShipDate < DateTime.Now)
            {
                ordersArr[i].DeliveryDate = ordersArr[i].ShipDate + new TimeSpan(0, 0, Config.rand.Next(30));
            }
        }

        for(int i = 0; i < 40; i++)
        {
            addOrderItem();
        }

    }

    internal class Config
    {

        static internal int IdRunNum;
        public int GetIdRunNum
        {
            get { return ++IdRunNum; }
        }


        static internal Random rand = new Random(DateTime.Now.Millisecond);

        //indexs for the next clear space in the array
        internal static int productsSize = 0;
        internal static int ordersSize = 0;
        internal static int ordersItemSize = 0;
    }
}