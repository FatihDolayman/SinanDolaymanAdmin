using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinanDolayman.Controllers
{
    public class HomeController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        public ActionResult Index()
        {
           var makaleler = db.Articles.OrderByDescending(a=>a.Id).Take(3);

            return View(makaleler);
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