
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    void Print(OrderItem ot1);

    IEnumerable<OrderItem> ReadAll();
}
