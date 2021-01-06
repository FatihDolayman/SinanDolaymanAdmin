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
    public class BookController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Book
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Books.Where(a => a.Name.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList());
            }
            else
            {
                return View(db.Books.OrderByDescending(a => a.CreateDate).ToList());
            }
        }
       

            // GET: Book/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
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
