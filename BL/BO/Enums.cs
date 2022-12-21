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
    Beauty,
    None
}

public enum OrderStatus
{
    ConfirmedOrder, //when the costumer paied for the order
    Sent, //when the order was sent
    Provided, //when the costumer recived the order
    None
}

