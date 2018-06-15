using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerShopping.Models;
using FlowerShopping.ModelView;

namespace FlowerShopping.Controllers
{
    public class HomeController : Controller
    {
        FlowersEntities1 db = new FlowersEntities1();
        public ActionResult Index()
        {
            var productnew = (from p in db.Product select p).OrderByDescending(p => p.AddTime).Take(5);
            var producthot = (from p in db.Product select p).OrderByDescending(p => p.SalesNumber).Take(8);
            HomeViewModel index = new ModelView.HomeViewModel();
            index.productNew = productnew;
            index.productHot = producthot;

            return View(index);
        }

        public ActionResult Login()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}