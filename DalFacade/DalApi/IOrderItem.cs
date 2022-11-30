
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem?>
{
    void Print(OrderItem ot1);

    IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? predicate = null);

    public OrderItem ReadProductId(int productId); //read with product id
}
