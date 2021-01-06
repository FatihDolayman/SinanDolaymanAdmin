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
    public class VideoController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Video
        public ActionResult Index()
        {
            var videos = db.Videos.Include(v => v.Category);
            return View(videos.ToList());
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

        // GET: Video/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name");
            return View();
        }

        // POST: Video/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,VideoPath,Summary,CreateDate,ModifyDate,CategoryId")] Video video)
        {
            if (ModelState.IsValid)
            {
                video.CreateDate = DateTime.Now;
                video.ModifyDate = DateTime.Now;
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name", video.CategoryId);
            return View(video);
        }

        // GET: Video/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name", video.CategoryId);
            return View(video);
        }

        // POST: Video/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,Summary,CreateDate,ModifyDate,CategoryId")] Video video)
        {
            if (ModelState.IsValid)
            {
                Video dbVideo = new Video();
                dbVideo = db.Videos.Find(video.Id);

                dbVideo.Title = video.Title;
                dbVideo.VideoPath = video.VideoPath;
                dbVideo.Summary = video.Summary;
                dbVideo.ModifyDate = DateTime.Now;
                db.Entry(dbVideo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name", video.CategoryId);
            return View(video);
        }

        // GET: Video/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            db.Videos.Remove(video);
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
