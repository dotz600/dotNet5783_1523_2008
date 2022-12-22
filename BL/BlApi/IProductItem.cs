using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlApi;
/// <summary>
/// Fields:
/// * ID
///* Name 
///* Price
///* Category
///* InStock
///* Amount
/// </summary>
public interface IProductItem
{
    IEnumerable<BO.ProductItem> ReadAll(Func<BO.ProductItem, bool>? predicate = null);
}
