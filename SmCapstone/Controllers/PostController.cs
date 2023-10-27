using SmCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SmCapstone.Controllers
{
    public class PostController : Controller
    {
        private Context db = new Context();
        // GET: Post
        [HttpGet]
        public ActionResult InsertNewPost()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InsertNewPost(Products p, HttpPostedFileBase Img1, HttpPostedFileBase img2, HttpPostedFileBase img3)
        {
            ModelState.Remove("IsSold");
            Users u = Session["currentUser"] as Users;
            p.IdUser = u.IdUser;
            p.IsSold = false;
            if (ModelState.IsValid)
            {
                if (Img1 != null && Img1.ContentLength > 0)
                {
                    string nomeFile = Img1.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets/productsImgs"), nomeFile);
                    Img1.SaveAs(path);
                    p.Img1 = nomeFile;
                }
                if (img2 != null && img2.ContentLength > 0)
                {
                    string nomeFile = img2.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets/productsImgs"), nomeFile);
                    img2.SaveAs(path);
                    p.img2 = nomeFile;
                }
                if (img3 != null && img3.ContentLength > 0)
                {
                    string nomeFile = img3.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets/productsImgs"), nomeFile);
                    img3.SaveAs(path);
                    p.img3 = nomeFile;
                }
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("ProfilePage", "Users");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ProductDetails(int id)
        {
            Products p = db.Products.Find(id);
            return View(p);
        }
        public ActionResult PartialProductInfo(int id)
        {
            return PartialView(db.Products.Find(id));
        }
        [HttpGet]
        public ActionResult MakeOffer(int id)
        {
            Session["Product"] = db.Products.Find(id);
            return View();
        }
        [HttpPost]
        public ActionResult MakeOffer([Bind(Exclude = "IsAccepted,BidDate")] Bids b)
        {
            Users currentUser = Session["currentUser"] as Users;
            Products p = Session["Product"] as Products;
            int id = p.IdProduct;
            b.BidDate = DateTime.Now;
            b.IdProduct = id;
            b.IdUser = currentUser.IdUser;
            if (ModelState.IsValid)
            {
                db.Bids.Add(b);
                db.SaveChanges();
                return RedirectToAction("ProductDetails", new
                {
                    id
                });
            }
            else
            {
                return View();
            }

        }
        public ActionResult DeclineOffer(int id)
        {
            Bids b = db.Bids.Find(id);
            b.IsAccepted = false;
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ProfilePage", "Users");
        }
        public ActionResult AcceptOffer(int id)
        {
            Bids b = db.Bids.Find(id);
            b.IsAccepted = true;
            Products p = db.Products.Find(b.IdProduct);
            p.IsSold = true;
            db.Entry(b).State = EntityState.Modified;
            db.SaveChanges();
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            Transactions t = new Transactions();
            t.IsPaid = false;
            t.IdBid = b.IdBid;
            db.Transactions.Add(t);
            db.SaveChanges();
            return RedirectToAction("ProfilePage", "Users");
        }
        public ActionResult DeleteOffer(int id)
        {
            Bids b = db.Bids.Find(id);
            db.Bids.Remove(b);
            db.SaveChanges();
            return RedirectToAction("ProfilePage", "Users");
        }
        [HttpGet]
        public ActionResult CheckOut(int id)
        {
            Transactions t = db.Transactions.Find(id);
            return View(t);
        }
        [HttpPost]
        public ActionResult CheckOut(Transactions t)
        {
            if (ModelState.IsValid)
            {
                t.IsPaid = true;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfilePage", "Users");
            }
            else
            {
                return View(t);
            }
        }


    }
}