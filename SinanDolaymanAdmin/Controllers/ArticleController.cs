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
    public class ArticleController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Article
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,CoverImage,Summary,Author")] Article article, HttpPostedFileBase image)
        {
            if (image == null || image.ContentLength == 0)
            {
                ViewBag.FileError = "Lütfen bir dosya yükleyiniz";
                return View(article);
            }
            if (image.ContentLength > 5 * 1024 * 1024)
            {
                ViewBag.FileError = "Dosya boyutu 5 MB'dan büyük olamaz";
                return View(article);
            }

            if (!ModelState.IsValid)
            {
                return View(article);
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
                DateTime utc = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utc, tzi);

                article.CreateDate = turkeyDateTime;
                article.CoverImage = uploadResult.Url.ToString();
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.FileError = "Resim yükleme başarısız";
                return View(article);
            }

        }

        // GET: Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,CreateDate,ModifyDate,CoverImage,Summary")] Article article, HttpPostedFileBase image)
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
                        return View(article);
                    }

                    fileName = Guid.NewGuid() + ".png";
                    image.SaveAs(Path.Combine(Server.MapPath("/SiteResimleri/"), fileName));

                    article.CoverImage = "/SiteResimleri/" + fileName;
                }

                Article dbArticle = new Article();
                dbArticle = db.Articles.Find(article.Id);

                dbArticle.Content = article.Content;
                dbArticle.Title = article.Title;
                dbArticle.Content = article.Content;
                dbArticle.CoverImage = article.CoverImage;
                dbArticle.ModifyDate = DateTime.Now;
                db.Entry(dbArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
