using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DAL;
using Entities;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static SinanDolaymanAdmin.Helper.CloudinaryHelper;

namespace SinanDolaymanAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class InterviewController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();
        private object interview;

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] Interview interview, HttpPostedFileBase image)
        {
            if (image == null || image.ContentLength == 0)
            {
                ViewBag.FileError = "Lütfen bir dosya yükleyiniz";
                return View(interview);
            }

            if (image.ContentLength > 5 * 1024 * 1024)
            {
                ViewBag.FileError = "Dosya boyutu 5 MB'dan büyük olamaz";
                return View(interview);
            }

            if (!ModelState.IsValid)
            {
                return View(interview);
            }
            Cloudinary cloudinary;
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);

            var UploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };
            var uploadResult = cloudinary.Upload(UploadParams, "raw");
            if (uploadResult != null)
            {
                interview.CreateDate = DateTime.Now;
                interview.CoverImage = uploadResult.Url.ToString();
                db.Interviews.Add(interview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.FileError = "Resim yükleme başarısız";
                return View(interview);
            }
            
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
