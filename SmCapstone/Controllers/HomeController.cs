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
            Session["Categories"] = db.Categories.ToList();

            return View(db.Products.ToList());
        }
        public ActionResult AllProducts()
        {
            

            if (Session["ProductList"] != null)
            {
                IEnumerable<Products> productList = Session["ProductList"] as IEnumerable<Products>;
                return View(productList);
            }
            else
            {
                TempData["NotFound"] = "Nessun annuncio corrispondente";
                return View(new List<Products>());
            }

        }
        public ActionResult Search(string productName)
        {
            List<Products> productList = db.Products.Where(p => p.Name.Contains(productName) && p.IsSold!=true).ToList();
            var formattedProduct = productList.Select(p => new
            {
                p.IdProduct,
                p.Name,
                p.Brand,
                p.Description,
                p.Price,
                p.Img1,
                p.img2,
                p.img3,
                p.IdUser,
                p.IdCategory,
               
            }).ToList();
            if (productList.Count > 0)
            {
                Session["ProductList"] = productList;
            }
            else
            {
                Session.Remove("ProductList");
            }
            return Json(formattedProduct, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Category(int id)
        {
            TempData["Title"] = db.Categories.Single(c=> c.IdCategory == id).Name.ToString();
            IEnumerable<Products> l = db.Products.Where(p => p.IdCategory == id && p.IsSold != true).ToList();
            return View(l);
        }
    }
}
