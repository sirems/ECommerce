using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models.DbModels;

namespace ECommerce.Models.ViewModels
{
    public class ShoppingCardVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
