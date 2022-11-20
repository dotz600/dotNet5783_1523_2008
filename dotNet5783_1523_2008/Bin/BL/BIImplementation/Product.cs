using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BIApi;
namespace BIImplementation;

internal class Product : IProduct
{
    private DalApi.IDal dal => new Dal.DalList();
    public void Create(BO.Product p)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Product Read(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Product Read(int id, BO.Cart myCart)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Product> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Product p)
    {
        throw new NotImplementedException();
    }
}
