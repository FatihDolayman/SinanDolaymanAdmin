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
            var video = db.Videos.Find(id);
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
        public static Cloudinary cloudinary;


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,CoverImage,Summary,VideoPath")] Entities.Video video, HttpPostedFileBase image)
        {
            if (video.CoverImage==null && (image == null || image.ContentLength == 0))
            {
                ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name");
                ViewBag.FileError = "Lütfen bir resim dosyası yükleyiniz";
                return View(video);
            }

            if (image != null && image.ContentLength > 5 * 1024 * 1024)
            {
                ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name");

                ViewBag.FileError = "Resim dosya boyutu 5 MB'dan büyük olamaz";
                return View(video);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name");
                return View(video);
            }
            if (image!=null )
            {
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
                    video.CoverImage = uploadResult.Url.ToString();
                }
                else
                {
                    ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name");

                    ViewBag.FileError = "Resim yükleme başarısız";
                    return View(video);
                }
            }
            video.CreateDate = DateTime.Now;
           
            db.Videos.Add(video);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Video/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.VideoCategories, "Id", "Name", video.CategoryId);
            return View(video);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,Summary,CategoryId")] Entities.Video video, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var dbVideo = new Entities.Video();
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
            var video = db.Videos.Find(id);
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
            var video = db.Videos.Find(id);
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
