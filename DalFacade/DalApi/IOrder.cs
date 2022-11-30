using DO;
namespace DalApi;

public interface IOrder : ICrud<Order>
{
    IEnumerable<Order?> ReadAll(Func< Order?, bool>? predicate = null);

    Order ReadIf(Func<Order?, bool> predicate);

}
