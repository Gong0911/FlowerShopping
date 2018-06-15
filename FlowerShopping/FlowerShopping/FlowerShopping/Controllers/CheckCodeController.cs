using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShopping.Controllers
{
    public class CheckCodeController : Controller
    {
        // GET: CheckCode
        public FileContentResult GetCode()
        {
            FlowerShopping.Checkcode.ValidateCode vCode = new Checkcode.ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}