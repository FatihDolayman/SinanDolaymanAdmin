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
    public class SoundController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Sound
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Sounds.Where(a => a.Title.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList());
            }
            else
            {
                return View(db.Sounds.OrderByDescending(a => a.CreateDate).ToList());
            }
        }

        // GET: Sound/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sound sound = db.Sounds.Find(id);
            if (sound == null)
            {
                return HttpNotFound();
            }
            var comments = db.Comments.AsNoTracking().Where(a => a.Module == Module.Sound && a.ModuleId == id).OrderByDescending(a => a.Date).ToList();
            ViewBag.Comments = comments;
            return View(sound);
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
