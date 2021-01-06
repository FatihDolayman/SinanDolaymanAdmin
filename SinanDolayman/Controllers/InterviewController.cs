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
    public class InterviewController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Interview
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Interviews.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList());
            }
            else
            {
                return View(db.Interviews.OrderByDescending(a => a.CreateDate).ToList());
            }
        }

        // GET: Interview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interview interview = db.Interviews.Find(id);
            if (interview == null)
            {
                return HttpNotFound();
            }
            return View(interview);
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
