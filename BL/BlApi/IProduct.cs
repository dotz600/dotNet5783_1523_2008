

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// Functions:
/// * Returns list of ProductForList objects with all of the products from the DL
/// * for manager screen, read one product according to ID
/// * for buyer screen, read one product according to ID
/// * for manager screen, create new product in the DL
/// * for manager screen, delete one product from DL according to ID
/// * for manager screen, update one product in DL
/// Fields:
///* ID
///* Name 
///* Price
///* Category
///* InStock
/// </summary>
public interface IProduct 
{
    
    IEnumerable<ProductForList?> ReadAll();//Returns list of ProductForList objects with all of the products from the DL
    Product Read(int id); //for manager screen, read one product according to ID
    ProductItem Read(int id, Cart myCart); //for buyer screen, read one product according to ID
    void Create(Product p);//for manager screen, create new product in the DL
    void Delete(int id);//for manager screen, delete one product from DL according to ID
    void Update(Product p);//for manager screen, update one product in DL

}
