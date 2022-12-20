//Data source class,
//Initializes the arrays with random data
//10 products, 20 orders, 40 order items
using DO;
using System.Security.Cryptography;

namespace Dal;

internal static class DataSource
{
    internal static readonly int s_randomNum = 100000;

    internal static List<Product?> s_productsArr;
    internal static List<Order?> s_ordersArr;
    internal static List<OrderItem?> s_ordersItemArr;

    
    static DataSource()//cnsrt
    {
        s_productsArr = new List<Product?>();
        s_ordersArr = new List<Order?>();
        s_ordersItemArr = new List<OrderItem?>();
        s_Initialize(); 
        
    }

    private static void addOrder(Order o1)
    {
        //update the dates
        o1.OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//time now - random time
        o1.ShipDate = o1.OrderDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
        if (o1.ShipDate < DateTime.Now)//if true - the order has been sent and need to update delivery date
            o1.DeliveryDate = o1.ShipDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));

        s_ordersArr.Add(o1);
    }

    private static void AddOrderItem(int i = 1)//add one orderItem
    {

        int amount = Config.rand.Next(1, 5);
         
        OrderItem ot1 = new OrderItem();
        ot1.ProductID = s_productsArr[i % 10].Value.ID;//alreday added 10 product to the list 
        ot1.OrderID = s_ordersArr[i % 20].Value.ID; // allready added 20 orders to the list
        ot1.Amount = amount;//amount is random number
        ot1.Price = s_productsArr[i % 10].Value.Price * amount;
        s_ordersItemArr.Add(ot1);

    }

    private static void AddProduct(Product p1)
    {
        s_productsArr.Add(p1);
    }

    private static void s_Initialize()
    {
        //adding 10 products.
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "doughnut", Category = Categories.Bakery, InStock = 15, Price = 5 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "chicken", Category = Categories.Meat, InStock = 25, Price = 30 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "Yellow cheese", Category = Categories.Deli, InStock = 12, Price = 33 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "bean", Category = Categories.Frozen, InStock = 7, Price = 16 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "mop", Category = Categories.Cleaning, InStock = 3, Price = 22 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "cows milk", Category = Categories.Dairy, InStock = 45, Price = 4.99 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "gum", Category = Categories.Sweets, InStock = 75, Price = 0.99 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "makeup", Category = Categories.Beauty, InStock = 15, Price = 7 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "bread", Category = Categories.Bakery, InStock = 50, Price = 5.99 });
        AddProduct( new Product { ID = Config.rand.Next(100000, 1000000), Name = "mince", Category = Categories.Meat, InStock = 0, Price = 42.5 });



        //adding 20 orders. 
        for (char i = 'a'; i < 'a' + 20; i++)
        {
            addOrder(new Order
            {
                ID = Config.getIdRunNum(),
                CustomerName = "Avi" + i,
                CustomerEmail = "Avi" + i + "@gmail.com",
                CustomerAdress = "jerusalem" + i,
                OrderDate = DateTime.MinValue,
                ShipDate = DateTime.MinValue,
                DeliveryDate = DateTime.MinValue
            });
        }

        //adding 40 orderItem
        for (int i = 1; i < 41; i++)
        {
            AddOrderItem(i);
        }

    }

    internal class Config//inner calss
    {

        internal static int s_idRunNum = 1000;
        internal static int getIdRunNum()
        {
            return ++s_idRunNum;
        }
        static internal Random rand = new Random(DateTime.Now.Millisecond);
        
    }
}
