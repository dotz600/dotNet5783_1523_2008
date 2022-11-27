using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
///for order tracking screen
namespace BO;

public class OrderTracking
{
    public struct DateAndStatus
    {
        public DateTime? dt; 
        public OrderStatus os;
        public override string ToString() => $@"
        Date & Time : {dt}
        Order status: {os}";
    };


    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<DateAndStatus> Events { get; set;}

    
    public override string ToString() => $@"
    ID :  {ID}
    OrderStatus : {Status}";

}
