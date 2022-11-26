using BIApi;

namespace BIImplementation;

internal class ProductForList : IProductForList
{
    public BO.ProductForList DOproductToBOproductForList(DO.Product DOproduct) //get DoProduct and convert it to product for list
    {

        return new BO.ProductForList 
        {
            Price = DOproduct.Price, 
            ID = DOproduct.ID, Name = DOproduct.Name, 
            Category = (BO.Categories)DOproduct.category 
        };
    }
}
