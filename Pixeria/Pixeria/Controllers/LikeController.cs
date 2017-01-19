using Pixeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pixeria.Controllers
{
    public class LikeController : Controller
    {
        private Entities db = new Entities();
       
        public JsonResult Like(int id)
        {
            int userId = db.User.ToList().Where(x => x.Username == Session["user"].ToString()).Select(x => x.Id).First();
            string status;
            if (db.Like.Where(x => x.DokumentId == id && x.UserId == userId).Count() == 0)
            {
                status = "New";
                Like like = new Like();
                like.DokumentId = id;
                like.UserId = userId;
                db.Like.Add(like);
            }
            else
            {
                status = "Del";
                db.Like.Remove(db.Like.Where(x => x.DokumentId == id && x.UserId == userId).First());
            }
            db.SaveChanges();
            ViewBag.Count = db.Like.Where(x => x.DokumentId == id).Count();
            return Json(status);
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