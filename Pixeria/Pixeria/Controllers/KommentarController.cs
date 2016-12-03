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
    public class KommentarController : Controller
    {
        private Entities db = new Entities();

        // GET: Kommentars
        public ActionResult Index()
        {
            var kommentar = db.Kommentar.Include(k => k.Dokument).Include(k => k.User);
            return View(kommentar.ToList());
        }

        // GET: Kommentars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kommentar kommentar = db.Kommentar.Find(id);
            if (kommentar == null)
            {
                return HttpNotFound();
            }
            return View(kommentar);
        }

        // GET: Kommentars/Create
        public ActionResult Create()
        {
            ViewBag.DokumentId = new SelectList(db.Dokument, "Id", "Pfad");
            ViewBag.UserId = new SelectList(db.User, "Id", "Username");
            return View();
        }

        // POST: Kommentars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,UserId,DokumentId")] Kommentar kommentar)
        {
            if (ModelState.IsValid)
            {
                db.Kommentar.Add(kommentar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DokumentId = new SelectList(db.Dokument, "Id", "Pfad", kommentar.DokumentId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Username", kommentar.UserId);
            return View(kommentar);
        }

        // GET: Kommentars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kommentar kommentar = db.Kommentar.Find(id);
            if (kommentar == null)
            {
                return HttpNotFound();
            }
            ViewBag.DokumentId = new SelectList(db.Dokument, "Id", "Pfad", kommentar.DokumentId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Username", kommentar.UserId);
            return View(kommentar);
        }

        // POST: Kommentars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,UserId,DokumentId")] Kommentar kommentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kommentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DokumentId = new SelectList(db.Dokument, "Id", "Pfad", kommentar.DokumentId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Username", kommentar.UserId);
            return View(kommentar);
        }

        // GET: Kommentars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kommentar kommentar = db.Kommentar.Find(id);
            if (kommentar == null)
            {
                return HttpNotFound();
            }
            return View(kommentar);
        }

        // POST: Kommentars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kommentar kommentar = db.Kommentar.Find(id);
            db.Kommentar.Remove(kommentar);
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
