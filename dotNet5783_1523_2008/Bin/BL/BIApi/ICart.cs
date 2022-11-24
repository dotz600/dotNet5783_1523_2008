using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BIApi;

/// <summary>
/// ICart --- TO DO --- Complete the interface summary
/// </summary>
public interface ICart 
{
    Cart Create(Cart cart, int productId);
    void CartConfirmation(Cart cart, string name, string email, string adress);
    Cart Update(Cart cart, int productId, int amount);
}
