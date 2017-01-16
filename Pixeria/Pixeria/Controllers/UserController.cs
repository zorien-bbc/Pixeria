using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pixeria.Models;

namespace Pixeria.Controllers
{
    public class UserController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Profile()
        {
            if (Session["user"] != null)
            {
                User user = db.User.ToList().Where(x => x.Username == Session["user"].ToString()).First();
                return View(user.Dokument);
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser([Bind(Include = "Id,username,passwd")] User user)
        {
            foreach (User u in db.User.ToList())
            {
                if (u.Username.Equals(user.Username))
                {

                    if (u.Passwd.Equals(user.Passwd))
                    {
                        Session["user"] = user.Username;
                        return RedirectToAction("Index", "Home", null);
                    }
                }
            }
            ModelState.AddModelError("", "Login attempt failed.");
            return View("Login");

        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home", null);
        }
        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: User/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Username,Passwd")] User user)
        {
            if (UserAlreadyExists(user.Username))
            {
                ModelState.AddModelError("", "Username already exists");
                return View("Register");
            }
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
        public bool UserAlreadyExists(string name)
        {
            foreach (User user in db.User.ToList())
            {
                if (user.Username.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Passwd")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Passwd")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
