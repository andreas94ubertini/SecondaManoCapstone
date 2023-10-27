using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmCapstone.Models;

namespace SmCapstone.Controllers
{
    public class UsersController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
                Session["currentUser"] = currentUser;
            }

            return View();
        }
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser([Bind(Exclude = "Role,Date")] Users u, HttpPostedFileBase Img)
        {
            ModelState.Remove("Date");
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                Users user = db.Users.SingleOrDefault(x => x.Username == u.Username);
                if (user == null)
                {
                    if (Img != null && Img.ContentLength > 0)
                    {
                        string nomeFile = Img.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/assets/profileImgs"), nomeFile);
                        Img.SaveAs(path);
                        u.Img = nomeFile;
                    }
                    else
                    {
                        u.Img = "";
                    }
                    u.Role = "User";
                    u.Date = DateTime.Now;
                    db.Users.Add(u);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Username già utilizzato";
                    return View();
                }
            }
            else return View();
        }
        [HttpGet]
        public ActionResult EditUser()
        {
            Users u = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            TempData["img"] = u.Img;
            return View(u);
        }
        [HttpPost]
        public ActionResult EditUser([Bind(Exclude = "Role")] Users u, HttpPostedFileBase Img)
        {
            
            ModelState.Remove("Date");
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                if (Img != null && Img.ContentLength > 0)
                {
                    string nomeFile = Img.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets/profileImgs"), nomeFile);
                    Img.SaveAs(path);
                    u.Img = nomeFile;
                }
                else
                {
                    u.Img = TempData["img"].ToString();
                }
                u.Role = "User";
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                Session["currentUser"] = u;
                return RedirectToAction("Index");
            }
            else
            {
                return View(u);
            }
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserLogIn u)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Users.SingleOrDefault(x => x.Username == u.Username);
                if (user.Username != null && user.Psw == u.Psw)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("currentUser");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ProfilePage()
        {

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    Users u = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
                    Session["currentUser"] = u;
                    return View(u);
                }
                else
                {
                    return RedirectToAction("LogIn");
                }
            
        }

    }
}
