using SmCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmCapstone.Controllers
{
    public class MessageController : Controller
    {
        private Context db = new Context();
        // GET: Message
        public ActionResult MyMessages(int id)
        {
            return View(db.Chats.Find(id));
        }
        [HttpGet]
        public ActionResult NewMessage(int id)
        {
            Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            TempData["IdUserToSend"] = id;
            if (currentUser.Chats.Count > 0)
            {
                foreach (Chats c in currentUser.Chats)
                {
                    if (c.IdUserTwo == id)
                    {
                        TempData["IdChat"] = c.IdChat;
                        return RedirectToAction("SendResponse", new
                        {
                            id
                        });
                        
                    }
                    else
                    {
                        Chats chats = new Chats();
                        chats.IdUserTwo = id;
                        chats.IdUserOne = currentUser.IdUser;
                        db.Chats.Add(chats);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                Chats chats = new Chats();
                chats.IdUserTwo = id;
                chats.IdUserOne = currentUser.IdUser;
                db.Chats.Add(chats);
                db.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Messages m)
        {
            Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            int IdChat;
            if (TempData["IdChat"] != null)
            {
                IdChat = Convert.ToInt32(TempData["IdChat"]);
            }
            else
            {
                int IdToCheck = Convert.ToInt32(TempData["IdUserToSend"]);
                Chats ch = db.Chats.First(c => c.IdUserTwo == IdToCheck && c.IdUserOne == currentUser.IdUser);
                IdChat = ch.IdChat;
            }

            m.Date = DateTime.Now;
            m.IdUserSender = currentUser.IdUser;
            m.IdUserReceiving = Convert.ToInt32(TempData["IdUserToSend"]);
            m.IsRead = false;
            m.IdChat = IdChat;
            if (ModelState.IsValid)
            {
                db.Messages.Add(m);
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        public ActionResult PartialResponse(int id)
        {
            TempData["IdChat"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult PartialResponse(Messages m)
        {
            Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            m.IdChat = Convert.ToInt32(TempData["IdChat"]);
            m.Date = DateTime.Now;
            m.IsRead = false;
            m.IdUserSender = currentUser.IdUser;

            return View();
        }
        public ActionResult RenderChat(int id)
        {
            Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            ViewBag.IdCurrentUser = currentUser.IdUser;
            Chats c = db.Chats.Find(id);
            int IdUserReceiver = 0;
            foreach (Messages m in c.Messages)
            {
                if (m.IdUserSender != currentUser.IdUser)
                {
                    IdUserReceiver = m.IdUserSender;
                    break;
                }

            }
            foreach(Messages m in c.Messages)
            {
                if(m.IdUserReceiving == currentUser.IdUser && m.IsRead == false)
                {
                    m.IsRead = true;
                    db.Entry(m).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            TempData["IdUserReceiver"] = IdUserReceiver;
            return PartialView(c);
        }
        [HttpGet]
        public ActionResult SendResponse(int id)
        {
            TempData["IdChat"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult SendResponse(Messages m)
        {
            Users currentUser = db.Users.First(us => us.Username == HttpContext.User.Identity.Name);
            m.Date = DateTime.Now;
            int id = Convert.ToInt32(TempData["IdChat"]);
            m.IdChat = id;
            m.IdUserSender = currentUser.IdUser;
            m.IdUserReceiving = Convert.ToInt32(TempData["IdUserReceiver"]);
            m.IsRead = false;
            if(ModelState.IsValid)
            {
                db.Messages.Add(m);
                db.SaveChanges();
            }
            return RedirectToAction("SendResponse", new
            {
                id
            }) ;
        }

    }
}