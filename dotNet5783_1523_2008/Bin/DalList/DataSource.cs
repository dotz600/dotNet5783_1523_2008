
using DO;
namespace Dal;

internal static class DataSource
{
    static DataSource() { s_Initialize(); }
    
    static readonly int RandomNum;

    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] ordersItemArr = new OrderItem[200];

   
    private static void addOrder()
    {
     
    }
    private static void addOrderItem()
    {

    }
    private static void addProduct()
    {

    }
    private static void s_Initialize()
    {
        addOrder();
        
    }
    internal class Config
    {

        //indexs for the next clear space in the array
        internal static int productsSize = 0;
        internal static int ordersSize = 0;
        internal static int ordersItemSize = 0;
    }
}
