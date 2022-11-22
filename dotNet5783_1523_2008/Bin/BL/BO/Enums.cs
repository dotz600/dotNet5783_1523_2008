using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum Categories
{
    Bakery,
    Meat,
    Deli,
    Frozen,
    Cleaning,
    Dairy,
    Grocery,
    Sweets,
    Beauty
}

public enum OrderStatus
{
    ConfirmedOrder, //when the costumer paied for the order
    Sent, //
    Provided //when the costumer recived the order
}

