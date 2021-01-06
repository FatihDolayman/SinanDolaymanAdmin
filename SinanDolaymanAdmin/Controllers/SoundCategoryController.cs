using DAL;
using Entities;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class SoundCategoryController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: SoundCategory
        public ActionResult Index()
        {
            return View(db.SoundCategories.ToList());
        }

        // GET: SoundCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundCategory soundCategory = db.SoundCategories.Find(id);
            if (soundCategory == null)
            {
                return HttpNotFound();
            }
            return View(soundCategory);
        }

        // GET: SoundCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoundCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SoundCategory soundCategory)
        {
            if (ModelState.IsValid)
            {
                db.SoundCategories.Add(soundCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(soundCategory);
        }

        // GET: SoundCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundCategory soundCategory = db.SoundCategories.Find(id);
            if (soundCategory == null)
            {
                return HttpNotFound();
            }
            return View(soundCategory);
        }

        // POST: SoundCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SoundCategory soundCategory)
        {
            if (ModelState.IsValid)
            {
                SoundCategory dbSoundCategory = new SoundCategory();
                dbSoundCategory = db.SoundCategories.Find(soundCategory.Id);
                dbSoundCategory.Name = soundCategory.Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(soundCategory);
        }

        // GET: SoundCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoundCategory soundCategory = db.SoundCategories.Find(id);
            if (soundCategory == null)
            {
                return HttpNotFound();
            }
            return View(soundCategory);
        }

        // POST: SoundCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SoundCategory soundCategory = db.SoundCategories.Find(id);
            db.SoundCategories.Remove(soundCategory);
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
