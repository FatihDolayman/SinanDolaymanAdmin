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
using PagedList;
using PagedList.Mvc;


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
            var comments = db.Comments.AsNoTracking().Where(a => a.Module == Module.Article && a.ModuleId == id).OrderByDescending(a=>a.Date).ToList();
            ViewBag.Comments = comments;
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
