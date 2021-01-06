using DAL;
using Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class InterviewController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Interview
        public ActionResult Index()
        {
            return View(db.Interviews.ToList());
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

        // GET: Interview/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interview/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,CreateDate,ModifyDate")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                interview.CreateDate = DateTime.Now;
                interview.ModifyDate = DateTime.Now;
                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interview);
        }

        // GET: Interview/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Interview/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,CreateDate,ModifyDate")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                Interview dbInterview = new Interview();
                dbInterview = db.Interviews.Find(interview.Id);

                dbInterview.Content = interview.Content;
                dbInterview.Title = interview.Title;
                dbInterview.Content = interview.Content;
                dbInterview.ModifyDate = DateTime.Now;
                db.Entry(dbInterview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interview);
        }

        // GET: Interview/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Interview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interview interview = db.Interviews.Find(id);
            db.Interviews.Remove(interview);
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
