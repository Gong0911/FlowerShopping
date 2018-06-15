using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlowerShopping.Models;
using FlowerShopping.ModelView;

namespace FlowerShopping.Controllers
{
    public class ProductController : Controller
    {
        FlowersEntities1 db = new FlowersEntities1();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        
        //商品详情页面
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }


         
            var productcomm = from m in db.Comment.Where(p => p.ProductID == id).OrderByDescending(p => p.CommentTime) select m;

            var index = new FlowerShopping.ModelView.ProductViewModel()
            {
                Products = product,
               
                productComm=productcomm,
             

            };
            return View(index);
        }

        //商品详情页的热销推荐页

        public ActionResult ProductHotList()
        {
            var producthot = (from p in db.Product select p).OrderByDescending(p => p.SalesNumber).Take(5);
            ProductViewModel index = new ModelView.ProductViewModel();
            index.productHot = producthot;
            return View(index);
        }


        //商品评论页
        [HttpPost]
        public ActionResult ProductComments(Comment proComm)
        {
            string commtextarea = Request["pingluntextarea"];
            int proid = Convert.ToInt32(Request["proid"]);

            if (ModelState.IsValid)
            {
                proComm.ProductID = proid;
                proComm.UserID = Convert.ToInt32(Session["UsersId"].ToString());
                proComm.CommContent = commtextarea;
                proComm.CommentTime = System.DateTime.Now;

                db.Comment.Add(proComm);
                db.SaveChanges();
                return Content("<script>;alert('评论成功!');history.go(-1)</script>");

            }

            return RedirectToAction("ProductDetail", "Product");

        }


        //添加购物车
        [HttpPost]
        public ActionResult AddToCart(Cart addCart)
        {
            int userid = Convert.ToInt32(Session["UsersId"]);
            int proid = Convert.ToInt32(Request["proid"]);
            int data = (from p in db.Cart where p.UserID == userid && p.ProductID == proid select p).Count();
            if (data > 0)
            {
                return Content("<script>alert('该商品已存在购物车');history.go(-1)</script>");
            }

            else
            {
                addCart.ProductID = proid;
                addCart.UserID = Convert.ToInt32(Session["UsersId"].ToString());

                addCart.UnitPrice = Convert.ToDecimal(Request.Form["price"]);
                addCart.Quantity = Convert.ToInt32(Request.Form["number"]);
                addCart.TotalPrice = addCart.Quantity * addCart.UnitPrice;
                addCart.CreatTime = System.DateTime.Now;

                db.Cart.Add(addCart);
                db.SaveChanges();

                return Content("<script>alert('添加成功');history.go(-1)</script>");
            }
        }
    }
}