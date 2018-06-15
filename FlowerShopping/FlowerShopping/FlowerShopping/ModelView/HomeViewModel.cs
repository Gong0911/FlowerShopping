using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowerShopping.Models;

namespace FlowerShopping.ModelView
{
    public class HomeViewModel
    {
        public Product Products { get; set; }
        public IEnumerable<Product> productHot { get; set; }
        public IEnumerable<Product> productNew { get; set; }
    }
}