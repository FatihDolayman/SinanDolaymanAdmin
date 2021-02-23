using DAL;
using Entities;
using SinanDolaymanAdmin.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        public ActionResult CommentsAreNotOk()
        {
           
            return View();
        }
       
        public ActionResult OnayBekleyenYorumlarPartial(int? id)
        {
            if (id!=-1&&id!=null)
            {
                var comment = db.Comments.Find(id);
                comment.IsOk = true;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
            }
            var comments = db.Comments.Where(a => a.IsOk == false).ToList();

            return PartialView(comments);
        }

        public ActionResult Sil(int id)
        {
            var comment = db.Comments.Find(id);
            if (comment!=null)
            {
                db.Entry(comment).State = EntityState.Deleted;
                db.SaveChanges();
            }
            
            var comments = db.Comments.Where(a => a.IsOk == false).ToList();
            return PartialView("OnayBekleyenYorumlarPartial", comments);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);



            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
