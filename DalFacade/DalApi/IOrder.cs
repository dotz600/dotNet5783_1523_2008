using DO;
namespace DalApi;

public interface IOrder : ICrud<Order?>
{
    IEnumerable<Order?> ReadAll(Func< Order?, bool>? predicate = null);
    void Print(Order o1);
}
