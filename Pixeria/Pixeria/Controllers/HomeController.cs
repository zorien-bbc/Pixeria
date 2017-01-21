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
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            var dokument = db.Dokument.Include(d =>d.User);
            IEnumerable<Dokument> dokumente = dokument;
            return View(dokumente.Reverse().ToList());
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Arbeitsprotokoll()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}