using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain
{
    public class Payment : BaseEntity
    {
        public double Amount { get; set; }
        public bool IsPayed { get; set; }
        public Order Order { get; set; }
    }
}
