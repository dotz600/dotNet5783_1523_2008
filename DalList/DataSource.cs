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

    private static void AddOrder(Order o1)
    {
        //update the dates
        o1.OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//time now - random time

        DateTime? tmp = o1.OrderDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//make random temp time
        if (tmp > DateTime.Now)//if the random time is bigger than now can add the order and return
        {
            s_ordersArr.Add(o1);
            return;
        }//else
        o1.ShipDate = tmp;
        tmp = o1.ShipDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship date + random time

        if (tmp > DateTime.Now)//if true - the order has been sent but dosnt arrive yet. manger need to update delivery date
        {
            s_ordersArr.Add(o1);
            return;
        }//else update delivery date
        o1.DeliveryDate = tmp;
        
        s_ordersArr.Add(o1);
    }

    private static void AddOrderItem(int i)//add one orderItem
    {

        int amount = Config.rand.Next(1, 5);
        int randomProduct = Config.rand.Next(10);
        OrderItem ot1 = new();

        ot1.ProductID = (int)s_productsArr[randomProduct]?.ID!;  //alreday added 10 product to the list 
        ot1.OrderID = (int)s_ordersArr[i % 20]?.ID!; // allready added 20 orders to the list
        ot1.Amount = amount;//amount is random number
        ot1.Price = (double)s_productsArr[randomProduct]?.Price! * amount;
        s_ordersItemArr.Add(ot1);
    }

    private static void AddProduct(Product p1)
    {
        s_productsArr.Add(p1);
    }

    private static void s_Initialize()
    {
        //adding 10 products.
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "doughnut", Category = Categories.Bakery, InStock = 15, Price = 5 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "chicken", Category = Categories.Meat, InStock = 25, Price = 30 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "Yellow cheese", Category = Categories.Deli, InStock = 12, Price = 33 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bean", Category = Categories.Frozen, InStock = 7, Price = 16 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mop", Category = Categories.Cleaning, InStock = 3, Price = 22 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "cows milk", Category = Categories.Dairy, InStock = 45, Price = 4.99 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "gum", Category = Categories.Sweets, InStock = 75, Price = 0.99 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "makeup", Category = Categories.Beauty, InStock = 15, Price = 7 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bread", Category = Categories.Bakery, InStock = 50, Price = 5.99 });
        AddProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mince", Category = Categories.Meat, InStock = 0, Price = 42.5 });



        //adding 20 orders. 
        for (char i = 'a'; i < 'a' + 20; i++)
        {
            AddOrder(new Order
            {
                ID = Config.getIdRunNum(),
                CustomerName = "Avi" + i,
                CustomerEmail = "Avi" + i + "@gmail.com",
                CustomerAdress = "jerusalem" + i,
                OrderDate = null,
                ShipDate = null,
                DeliveryDate = null
            });
        }

        //adding 40 orderItem
        for (int i = 0; i < 40; i++)
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
