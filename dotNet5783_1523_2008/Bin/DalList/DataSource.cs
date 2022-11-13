
using DO;

namespace Dal;
//Data source class,
//Initializes the arrays with random data
//10 products, 20 orders, 40 order items
internal static class DataSource
{
    static DataSource() { s_Initialize(); }

    internal static readonly int RandomNum = 100000;

    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] ordersItemArr = new OrderItem[200];


    private static void addOrder(Order o1)
    {
        ordersArr[Config.ordersSize] = o1;
        //update the dates
        ordersArr[Config.ordersSize].OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//time now - random time
        ordersArr[Config.ordersSize].ShipDate = ordersArr[Config.ordersSize].OrderDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
        if (ordersArr[Config.ordersSize].ShipDate < DateTime.Now)//if true - the order has been sent and need to update delivery date
        {
            ordersArr[Config.ordersSize].DeliveryDate = ordersArr[Config.ordersSize].ShipDate + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
        }
        Config.ordersSize++;//orders counter, from config class
    }

    private static void addOrderItem(int i = 0)//add one orderItem
    {

        int amount = Config.rand.Next(1, 5);
        ordersItemArr[Config.ordersItemSize].ProductID = productsArr[i % Config.productsSize].ID;//take product id from product arr
        ordersItemArr[Config.ordersItemSize].OrderID = ordersArr[i % Config.ordersSize].ID; //take order id from order arr
        ordersItemArr[Config.ordersItemSize].Amount = amount;//amount is random number
        if (Config.productsSize != 0)//just for safety
            ordersItemArr[Config.ordersItemSize].Price = productsArr[i % Config.productsSize].Price * amount;
        Config.ordersItemSize++;

    }

    private static void addProduct(Product p1)
    {
        productsArr[Config.productsSize++] = p1;
    }

    private static void s_Initialize()
    {
        //adding 10 products.
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "doughnut", category = Categories.Bakery, InStock = 15, Price = 5 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "chicken", category = Categories.Meat, InStock = 25, Price = 30 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "Yellow cheese", category = Categories.Deli, InStock = 12, Price = 33 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bean", category = Categories.Frozen, InStock = 7, Price = 16 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mop", category = Categories.Cleaning, InStock = 3, Price = 22 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "cows milk", category = Categories.Dairy, InStock = 45, Price = 4.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "gum", category = Categories.Sweets, InStock = 75, Price = 0.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "makeup", category = Categories.Beauty, InStock = 15, Price = 7 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "bread", category = Categories.Bakery, InStock = 50, Price = 5.99 });
        addProduct(new Product { ID = Config.rand.Next(100000, 1000000), Name = "mince", category = Categories.Meat, InStock = 0, Price = 42.5 });



        //adding 20 orders. 
        for (char i = 'a'; i <= 'a' + 20; i++)
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
            addOrderItem(i);
        }

    }

    internal class Config//inner calss
    {

        internal static int IdRunNum = 1000;
        internal static int getIdRunNum()
        {
            return ++IdRunNum;
        }
        static internal Random rand = new Random(DateTime.Now.Millisecond);
        //indexs for the next clear space in the array
        internal static int productsSize = 0;
        internal static int ordersSize = 0;
        internal static int ordersItemSize = 0;
    }
}
