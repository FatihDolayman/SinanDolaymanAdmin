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

namespace SinanDolaymanAdmin.Controllers
{
    public class CommentReplyController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: CommentReply
        public ActionResult Index()
        {
            return View(db.CommentReplies.ToList());
        }

        public ActionResult OnayBekleyenCevaplarPartial(int? id)
        {
            if (id != -1 && id != null)
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
            if (comment != null)
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
            CommentReply commentReply = db.CommentReplies.Find(id);
            if (commentReply == null)
            {
                return HttpNotFound();
            }
            return View(commentReply);
        }

        // GET: CommentReply/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentReply/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommentId,Content,IsOk,Date,Commenter,LikeCount,DislikeCount")] CommentReply commentReply)
        {
            if (ModelState.IsValid)
            {
                db.CommentReplies.Add(commentReply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentReply);
        }

        // GET: CommentReply/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: CommentReply/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommentId,Content,IsOk,Date,Commenter,LikeCount,DislikeCount")] CommentReply commentReply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentReply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentReply);
        }

        // GET: CommentReply/Delete/5
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

        // POST: CommentReply/Delete/5
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
