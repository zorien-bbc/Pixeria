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
        
        public JsonResult Kommentieren(int id,string text)
        {
            int userId = db.User.ToList().Where(x => x.Username == Session["user"].ToString()).Select(x => x.Id).First();
            Kommentar kommentar = new Kommentar();
            kommentar.Text = text;
            kommentar.UserId = userId;
            kommentar.DokumentId = id;
            db.Kommentar.Add(kommentar);
            db.SaveChanges();
            return Json("Ok");
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
