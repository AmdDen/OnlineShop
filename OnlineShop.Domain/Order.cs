using OnlineShop.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain
{
    public class Order : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public double Total { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public IList<OrderProducts> OrderProducts { get; set; }


        //public DateTime ShippingDate { get; set; }
        //public bool IsDelivered { get; set; }
    }
}
