using DAL;
using Entities;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class SoundController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Sound
        public ActionResult Index()
        {
            var sounds = db.Sounds.Include(s => s.Category);
            return View(sounds.ToList());
        }

        // GET: Sound/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sound sound = db.Sounds.Find(id);
            if (sound == null)
            {
                return HttpNotFound();
            }
            return View(sound);
        }

        // GET: Sound/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.SoundCategories, "Id", "Name");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Summary,CategoryId")] Sound sound, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string extension = String.Empty;
                string fileName = String.Empty;
                if (image != null && image.ContentLength > 0 && image.ContentLength < 2 * 1024 * 1024)
                {
                    extension = Path.GetExtension(image.FileName);

                    if (extension.Contains("pdf") || extension.Contains("doc") || extension.Contains("docx"))
                    {

                        ViewBag.Mesaj = "Desteklenmeyen dosya türü";
                        return View(sound);
                    }

                    fileName = Guid.NewGuid() + ".png";
                    image.SaveAs(Path.Combine(Server.MapPath("/SiteResimleri/"), fileName));

                    sound.CoverImage = "/SiteResimleri/" + fileName;
                }

                sound.CreateDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sound);
        }

        // GET: Sound/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sound sound = db.Sounds.Find(id);
            if (sound == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.SoundCategories, "Id", "Name", sound.CategoryId);
            return View(sound);
        }

        // POST: Sound/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Summary,CreateDate,ModifyDate,CategoryId")] Sound sound)
        {
            if (ModelState.IsValid)
            {
                Sound dbSound = new Sound();
                dbSound = db.Sounds.Find(sound.Id);

                dbSound.Title = sound.Title;
                dbSound.Summary = sound.Summary;
                dbSound.Path = sound.Path;
                dbSound.ModifyDate = DateTime.Now;
                db.Entry(dbSound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.SoundCategories, "Id", "Name", sound.CategoryId);
            return View(sound);
        }

        // GET: Sound/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sound sound = db.Sounds.Find(id);
            if (sound == null)
            {
                return HttpNotFound();
            }
            return View(sound);
        }

        // POST: Sound/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sound sound = db.Sounds.Find(id);
            db.Sounds.Remove(sound);
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
