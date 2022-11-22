using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BIApi;

namespace BIImplementation;

internal class ProductForList : IProductForList
{
    public BO.ProductForList DOproductToBOproductForList(DO.Product DOproduct) //get DoProduct and convert it to product for list
    {
        BO.ProductForList res = new BO.ProductForList();
        res.Price = DOproduct.Price;
        res.ID = DOproduct.ID;
        res.Name = DOproduct.Name;
        res.Category = (BO.Categories)DOproduct.category;
       
        return res;
    }
}
