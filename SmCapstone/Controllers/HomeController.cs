using SmCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmCapstone.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            TempData["Categories"] = db.Categories.ToList();

            return View(db.Products.ToList());
        }
        public ActionResult AllProducts()
        {
            return View(db.Products.ToList());
        }
    }
}
