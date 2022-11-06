
using DO;
using System.Xml.Linq;

namespace Dal;

internal static class DataSource
{
    static DataSource() { s_Initialize(); }
    
    static readonly int RandomNum;

    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] ordersItemArr = new OrderItem[200];

   
    private static void addOrder(Order o1)
    {
        ordersArr[Config.ordersSize++] = o1;
    }

    private static void addOrderItem()
    {
        ordersItemArr[Config.ordersItemSize].OrderID = Config.rand.Next(Config.ordersSize);
        ordersItemArr[Config.ordersItemSize].Amount = Config.rand.Next(5);
        int random = Config.rand.Next(Config.productsSize);//this random num needed for the 2 next line
        ordersItemArr[Config.ordersItemSize].ProductID = productsArr[random].ID;
        ordersItemArr[Config.ordersItemSize].Price = productsArr[random].Price * ordersItemArr[Config.ordersItemSize].Amount;
        Config.ordersItemSize++;
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


        for(int i = 0; i < 40; i++)
        {
            addOrderItem();
        }

    }
    internal class Config
    {
        static internal int IdRunNum = 1000;
        static internal Random rand = new Random(DateTime.Now.Millisecond);

        //indexs for the next clear space in the array
        internal static int productsSize = 0;
        internal static int ordersSize = 0;
        internal static int ordersItemSize = 0;
    }
}
