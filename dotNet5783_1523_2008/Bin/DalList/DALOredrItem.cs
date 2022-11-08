using DO;

namespace Dal;

public class DalOredrItem
{
    public int getOrderItem(OrderItem Ot)
    {
        DataSource.ordersItemArr[DataSource.Config.ordersItemSize] = Ot;
        return Ot.OrderID;
    }

    public OrderItem getOrderItem(int orderId, int proID)
    {
        for (int i = 0; i < DataSource.Config.ordersItemSize; i++)
            if (DataSource.ordersItemArr[i].OrderID == orderId && DataSource.ordersItemArr[i].ProductID == proID)
                return DataSource.ordersItemArr[i];

        throw new Exception("Order Item dont found");
    }
}
