using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entities;

namespace SinanDolayman.Controllers
{
    public class ArticleController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Article
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Articles.Where(a=>a.Title.Contains(searchTerm)||a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList());
            }
            else
            {
                return View(db.Articles.OrderByDescending(a => a.CreateDate).ToList());
            }
            
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
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
