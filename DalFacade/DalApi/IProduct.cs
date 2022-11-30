

using DO;

namespace DalApi;

public interface IProduct: ICrud<Product>
{

    IEnumerable<Product?> ReadAll(Func<Product?, bool>? predicate = null);

    Product ReadIf(Func<Product?, bool> predicate);

}
