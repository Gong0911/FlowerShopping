using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerShopping.Models;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace FlowerShopping.Controllers
{
    

    public class AdminController : Controller
    {
       private readonly FlowersEntities1 Context = new FlowersEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //账号登录
        [HttpPost]
        [AllowAnonymous]

        public ActionResult AdminLogin(Admin user, string returnUrl)
        {
            string ValidateCode = Request["checkCode"];
            if (ValidateCode != Session["ValidateCode"].ToString())
            {
                return Content("<script>;alert('验证码错误！');window.location.href='/Admin/AdminLogin'</script>");
            }
            try
            {

                var users = Context.Admin.Where(o => o.AdminName == user.AdminName).Where(o => o.AdminPwd == user.AdminPwd).FirstOrDefault();
                if (users != null)
                {
                    //以下代码将权限保存到Session
                    HttpContext.Session["AdminID"] = users.AdminID;
                    HttpContext.Session["AdminName"] = users.AdminName;
                    HttpContext.Session["AdminPwd"] = users.AdminPwd;
                  

                    return Content("<script>;alert('登录成功!前往后台!');window.location.href='/Admin/Index'</script>");

                }
                else
                {
                    return Content("<script>;alert('您输入的账号或密码有误!');history.go(-1)</script>");
                }


            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }


        }
    }
}