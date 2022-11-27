﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

/// <summary>
/// ICart --- TO DO --- Complete the interface summary
/// </summary>
public interface ICart 
{
    Cart Add(Cart cart, int productId);
    void CartConfirmation(Cart cart, string name, string email, string adress);
    Cart Update(Cart cart, int productId, int amount);
}