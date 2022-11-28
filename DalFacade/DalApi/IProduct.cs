

using DO;

namespace DalApi;

public interface IProduct: ICrud<Product>
{
    

    public void Print(Product p1);

    IEnumerable<Product> ReadAll();
}
