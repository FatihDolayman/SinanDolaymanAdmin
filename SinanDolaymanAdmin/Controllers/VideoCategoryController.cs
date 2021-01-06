using DAL;
using Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class VideoCategoryController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: VideoCategory
        public ActionResult Index()
        {
            return View(db.VideoCategories.ToList());
        }

        // GET: VideoCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoCategory videoCategory = db.VideoCategories.Find(id);
            if (videoCategory == null)
            {
                return HttpNotFound();
            }
            return View(videoCategory);
        }

        // GET: VideoCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] VideoCategory videoCategory)
        {
            if (ModelState.IsValid)
            {
                db.VideoCategories.Add(videoCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoCategory);
        }

        // GET: VideoCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoCategory videoCategory = db.VideoCategories.Find(id);
            if (videoCategory == null)
            {
                return HttpNotFound();
            }
            return View(videoCategory);
        }

        // POST: VideoCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] VideoCategory videoCategory)
        {
            if (ModelState.IsValid)
            {
                VideoCategory dbVideoCategory = new VideoCategory();
                dbVideoCategory = db.VideoCategories.Find(videoCategory.Id);

                dbVideoCategory.Name = videoCategory.Name;
                db.Entry(dbVideoCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoCategory);
        }

        // GET: VideoCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoCategory videoCategory = db.VideoCategories.Find(id);
            if (videoCategory == null)
            {
                return HttpNotFound();
            }
            return View(videoCategory);
        }

        // POST: VideoCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoCategory videoCategory = db.VideoCategories.Find(id);
            db.VideoCategories.Remove(videoCategory);
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
