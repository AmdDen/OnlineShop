using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Categoriy { get; set; }
        public IList<OrderProducts> OrderProducts { get; set; }

    }
}
