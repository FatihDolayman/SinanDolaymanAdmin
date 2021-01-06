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
    public class VideoController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Video
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Videos.Where(a => a.Title.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList());
            }
            else
            {
                return View(db.Videos.OrderByDescending(a => a.CreateDate).ToList());
            }
        }

        // GET: Video/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
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
