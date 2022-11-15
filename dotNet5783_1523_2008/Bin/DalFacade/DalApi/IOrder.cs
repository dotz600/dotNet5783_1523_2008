using DO;
namespace DalApi;

public interface IOrder : ICrud<Order>
{
    IEnumerable<Order> ReadAll();
    void Print(Order o1);
}
