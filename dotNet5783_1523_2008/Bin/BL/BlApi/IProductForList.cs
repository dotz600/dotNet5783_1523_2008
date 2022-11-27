using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
namespace BlApi;
/// <summary>
///Fields:
///* ID
///* Name 
///* Price
///* Category
/// </summary>
public interface IProductForList
{
    public BO.ProductForList DOproductToBOproductForList(DO.Product DOproduct);//get DoProduct and convert it to product for list

}
