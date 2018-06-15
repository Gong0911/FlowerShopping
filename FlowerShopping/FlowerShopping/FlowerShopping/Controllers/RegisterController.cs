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
    public class RegisterController : Controller
    {
        private readonly FlowersEntities1 Context = new FlowersEntities1();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }
        //账号注册
        [HttpPost]
     
        public ActionResult Register(UserInfo user, HttpPostedFileBase file)
        {

                var check_user = Context.UserInfo.Where(p => p.UserName == user.UserName).FirstOrDefault();
                if (check_user != null)
                {
                    return Content("<script>;alert('该账号已被人注册！请换一个！');history.go(-1)</script>");
                }
                try
                {
                    if (file != null)
                    {
                        string filePath = file.FileName;
                        string filename = filePath.Substring(filePath.LastIndexOf("//") + 1);
                        string serverpath = Server.MapPath(@"\Images\users\") + filename;
                        string relativepath = @"/Images/users/" + filename;
                        file.SaveAs(serverpath);
                        user.UeserImg = relativepath;
                    }

                    else
                    {
                        return Content("<script>;alert('请先上传图片！');history.go(-1)</script>");

                    }
                    if (ModelState.IsValid)
                    {
                        user.CreateTime = DateTime.Now;
                        Context.UserInfo.Add(user);
                        Context.SaveChanges();
                        return Content("<script>;alert('注册成功！');history.go(-1)</script>");

                    }
              
            }
                catch (DbEntityValidationException ex)
                {
                    return Content(ex.Message);
                }


            return View(user);

        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //账号登录
        [HttpPost]
        [AllowAnonymous]
      
        public ActionResult Login(UserInfo user,string returnUrl)
        {
            string ValidateCode = Request["checkCode"];
            if (ValidateCode != Session["ValidateCode"].ToString())
            {
                return Content("<script>;alert('验证码错误！');history.go(-1)</script>");
            }
            try
            {
             
                var users = Context.UserInfo.Where(o => o.UserName == user.UserName).Where(o => o.UserPwd == user.UserPwd).FirstOrDefault();
                if (users != null)
                {
                    //以下代码将权限保存到Session
                    HttpContext.Session["UsersId"] = users.UserID;
                    HttpContext.Session["UserName"] = users.UserName;
                    HttpContext.Session["UserPwd"] = users.UserPwd;
                    HttpContext.Session["UeserImg"] = users.UeserImg;
                                     
                    return Content("<script>;alert('登录成功!返回首页!');window.location.href='/Home/Index'</script>");
                              
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

        //账号退出
        public ActionResult LoginOut()
        {
            Session["UserName"] = null;
            return Content("<script>alert('账号退出成功!');window.location.href = document.referrer;</script>");

        }
         
    }
}