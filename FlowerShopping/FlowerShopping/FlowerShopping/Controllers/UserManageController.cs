using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowerShopping.Models;
using System.Net;
using System.Data.Entity;

namespace FlowerShopping.Controllers
{
    public class UserManageController : Controller
    {
        private FlowersEntities1 db = new FlowersEntities1();
        // GET: UserManage

        //显示所有用户的信息
        public ActionResult Index()
        {

            var users = from b in db.UserInfo
                        select b;
            return View(users);
        }

        //创建新用户
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateAntiForgeryToken]特性用来防止伪造的跨站请求，配合表单中的@Html.AntiForgeryToken()使用
        //对数据进行增删改时要防止csrf攻击！
        //该特性表示检测服务器请求是否被篡改。注意：该特性只能用于post请求，get请求无效。
        public ActionResult Create([Bind(Include = "UserID,UserName,UserPwd,UserPhone,UeserImg")] UserInfo user)
        {

            if (ModelState.IsValid)
            {
                db.UserInfo.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }


        //编辑用户
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInfo users = db.UserInfo.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: UsersManagement/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UserID,UserName,UserPwd,UserPhone,UeserImg")] UserInfo users)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(users).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(users);
        //}

        //删除用户

        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                var m = from n in db.UserInfo
                        where n.UserID == id
                        select n;
                db.UserInfo.Remove(m.FirstOrDefault());
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        ////编辑用户
        //public ActionResult Edit(int id)
        //{
        //    return View(GetUserId(id));
        //}

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here  
                UserInfo t = GetUserId(id);
                t.UserName = collection["UserName"];
                t.UserPwd = collection["UserPwd"];
                t.UserPhone = collection["UserPhone"];
                t.UeserImg = collection["UeserImg"];
                t.UserID = Convert.ToInt32(collection["UserID"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UserName,UserPwd,UserPhone,UeserImg")] UserInfo users)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(users).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(users);
        //}




        /// <summary>  
        /// 根据id返回一个T1对象  
        /// </summary>  
        /// <param name="id">id</param>  
        /// <returns>T1对象</returns>  
        private UserInfo GetUserId(int id)
        {
            var m = from n in db.UserInfo
                    where n.UserID == id
                    select n;
            return m.FirstOrDefault();
        }

    }
}