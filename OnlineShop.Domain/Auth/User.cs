using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Domain.Auth
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
