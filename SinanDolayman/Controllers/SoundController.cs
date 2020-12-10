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
        public ActionResult Index()
        {
            var sounds = db.Sounds.Include(s => s.Category);
            return View(sounds.ToList());
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
