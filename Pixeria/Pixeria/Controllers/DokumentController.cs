using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pixeria.Models;
using System.IO;

namespace Pixeria.Controllers
{
    public class DokumentController : Controller
    {
        private Entities db = new Entities();

        // GET: Dokument
        public ActionResult Upload()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Upload(string Titel, HttpPostedFileBase file)
        {
            if (string.IsNullOrEmpty(Titel))
            {
                return Json("Error"); 
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    User user = db.User.ToList().Where(x => x.Username == Session["user"].ToString()).First();
                    Dokument dokument = new Dokument();
                    dokument.Titel = Titel;
                    dokument.Pfad = "";
                    dokument.UserId = user.Id;
                    db.Dokument.Add(dokument);
                    db.SaveChanges();
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = dokument.Titel + dokument.Id + extension;
                    dokument.Pfad = "../resources/img/" + fileName;
                    db.Entry(dokument).State = EntityState.Modified;
                    db.SaveChanges();
                    var path = HttpContext.Server.MapPath("~/resources/img/");
                    file.SaveAs(path + fileName);
                }
                else
                {
                    ModelState.AddModelError("FileError", "File is required");
                   return Json("error");
                }
            }
            return Json("ok");
        }


        // GET: Dokument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokument dokument = db.Dokument.Find(id);
            if (dokument == null)
            {
                return HttpNotFound();
            }
            return View(dokument);
        }

        // GET: Dokument/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User, "Id", "Username");
            return View();
        }

        // POST: Dokument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pfad,Titel,UserId")] Dokument dokument)
        {
            if (ModelState.IsValid)
            {
                db.Dokument.Add(dokument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "Id", "Username", dokument.UserId);
            return View(dokument);
        }

        // GET: Dokument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokument dokument = db.Dokument.Find(id);
            if (dokument == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Username", dokument.UserId);
            return View(dokument);
        }

        // POST: Dokument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pfad,Titel,UserId")] Dokument dokument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dokument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Username", dokument.UserId);
            return View(dokument);
        }

        // GET: Dokument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokument dokument = db.Dokument.Find(id);
            if (dokument == null)
            {
                return HttpNotFound();
            }
            return View(dokument);
        }


        public JsonResult DeleteConfirmed(int id)
        {
            Dokument dokument = db.Dokument.Find(id);
            var shortPath = dokument.Pfad;
            shortPath = shortPath.Replace("../resources/img", "");
            var path = HttpContext.Server.MapPath("~/resources/img/");
            System.IO.File.Delete(path+shortPath);
            db.Dokument.Remove(dokument);
            db.SaveChanges();
            return Json("ok");
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
