using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerShopping.Models;
using FlowerShopping.ModelView;

namespace FlowerShopping.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        FlowersEntities1 db = new FlowersEntities1();
        public ActionResult Index()
        {

            var allcart = (from p in db.Cart select p);
            CartViewModel index = new ModelView.CartViewModel();
            index.allCart = allcart;
            return View(index);

           
        }

    }
}