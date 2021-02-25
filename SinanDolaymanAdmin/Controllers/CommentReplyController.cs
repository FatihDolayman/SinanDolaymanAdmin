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
    public class CommentReplyController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: CommentReply
        public ActionResult Index()
        {
            return View(db.CommentReplies.ToList());
        }

        public ActionResult CommentRepliesAreNotOk()
        {

            return View();
        }

        public ActionResult OnayBekleyenCevaplarPartial(int? id)
        {
            if (id != -1 && id != null)
            {
                var commentReply = db.CommentReplies.Find(id);
                commentReply.IsOk = true;
                db.Entry(commentReply).State = EntityState.Modified;
                db.SaveChanges();
            }
            var commentReplies = db.CommentReplies.Where(a => a.IsOk == false).ToList();

            return PartialView(commentReplies);
        }

        public ActionResult Sil(int id)
        {
            var commentReply = db.CommentReplies.Find(id);
            if (commentReply != null)
            {
                db.Entry(commentReply).State = EntityState.Deleted;
                db.SaveChanges();
            }

            var commentReplies = db.CommentReplies.Where(a => a.IsOk == false).ToList();
            return PartialView("OnayBekleyenCevaplarPartial", commentReplies);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentReply commentReply = db.CommentReplies.Find(id);



            if (commentReply == null)
            {
                return HttpNotFound();
            }
            return View(commentReply);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentReply commentReply = db.CommentReplies.Find(id);
            if (commentReply == null)
            {
                return HttpNotFound();
            }
            return View(commentReply);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentReply commentReply = db.CommentReplies.Find(id);
            db.CommentReplies.Remove(commentReply);
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