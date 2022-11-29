
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{

    IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? predicate = null);

    OrderItem ReadProductId(int productId); //read with product id

    OrderItem ReadIf(Func<OrderItem?, bool> predicate);

}
