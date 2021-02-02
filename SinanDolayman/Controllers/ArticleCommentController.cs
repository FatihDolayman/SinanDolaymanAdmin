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
    public class ArticleCommentController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: ArticleComment
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        // GET: ArticleComment/Details/5
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

       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string commenter, string comment,int articleId)
        {
            var commentAdded = new Comment();
            commentAdded.Commenter = commenter;
            commentAdded.Content = comment;
            commentAdded.Date = DateTime.Now;
            commentAdded.Module = Module.Article;
            commentAdded.ModuleId = articleId;
            db.Comments.Add(commentAdded);
            db.SaveChanges();           
            return RedirectToAction("Details","Article",new {id=articleId });
        }

        // GET: ArticleComment/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ArticleComment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,IsOk,Date,Commenter,LikeCount,DislikeCount,ModuleId,Module")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: ArticleComment/Delete/5
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

        // POST: ArticleComment/Delete/5
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
