using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowerShopping.Models;

namespace FlowerShopping.ModelView
{
    public class CartViewModel
    {
        public Cart Carts { get; set; }
        
        public IEnumerable<Cart> allCart { get; set; }

        public IEnumerable<Product> productCart { get; set; }


    }
}