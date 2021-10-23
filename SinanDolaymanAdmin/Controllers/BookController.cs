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
    public class BookController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Book
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }
        public static Cloudinary cloudinary;

        
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create([Bind(Include = "Id,Name,Description,Summary, CoverImage,DetailImage, Content,Path")] Book book, HttpPostedFileBase image, HttpPostedFileBase detailImage, HttpPostedFileBase bookPdf)
        {
            if ((image == null && book.CoverImage==null) || image.ContentLength == 0)
            {
                ViewBag.FileError = "Lütfen bir resim dosyası yükleyiniz";
                return View(book);
            }
            if (image.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Resim Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }
            if ((detailImage == null && book.DetailImage==null) || detailImage.ContentLength == 0)
            {
                ViewBag.FileError = "Lütfen bir detay resim dosyası yükleyiniz";
                return View(book);
            }
            if (detailImage.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Detay Resim Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);

            var UploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };
            var uploadResult = cloudinary.Upload(UploadParams, "raw");

            var UploadParams2 = new ImageUploadParams()
            {
                File = new FileDescription(detailImage.FileName, detailImage.InputStream)
            };
            var uploadResult2 = cloudinary.Upload(UploadParams2, "raw");

            if (uploadResult != null && uploadResult2 != null)
            {
                DateTime utc = DateTime.UtcNow;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utc, tzi);

                book.CreateDate = turkeyDateTime;
                book.CoverImage = uploadResult.Url.ToString();
                book.DetailImage = uploadResult2.Url.ToString();
            }
            else if(uploadResult == null)
            {
                ViewBag.FileError = "Kapak Resmi yükleme başarısız";
                return View(book);
            }
            else if (uploadResult2 == null)
            {
                ViewBag.FileError = "DEtay Resmi yükleme başarısız";
                return View(book);
            }          

            if (book.Path ==null && bookPdf == null )
            {
                ViewBag.FileError = "Lütfen bir pdf dosyası yükleyiniz";
                return View(book);
            }
            if (bookPdf != null && bookPdf.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Pdf Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }

            UploadParams.File = new FileDescription(bookPdf.FileName, bookPdf.InputStream);
            uploadResult = cloudinary.Upload(UploadParams, "raw");
            if (uploadResult != null)
            {
                book.Path = uploadResult.Url.ToString();
                book.CloudinaryPublicId = uploadResult.PublicId;
                db.Books.Add(book);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.FileError = "Pdf yükleme başarısız";
                return View(book);
            }

        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Summary,Content,Path, CoverImage, DetailImage")] Book book, HttpPostedFileBase image, HttpPostedFileBase detailImage, HttpPostedFileBase bookPdf)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            if ((image == null && book.CoverImage == null) || (image != null && image.ContentLength == 0))
            {
                ViewBag.FileError = "Lütfen bir resim dosyası yükleyiniz";
                return View(book);
            }
            if (image.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Resim Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }
            if ((detailImage == null && book.DetailImage == null) || (detailImage!=null && detailImage.ContentLength == 0))
            {
                ViewBag.FileError = "Lütfen bir detay resim dosyası yükleyiniz";
                return View(book);
            }
            if (detailImage.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Detay Resim Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);

            var UploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.InputStream)
            };
            var uploadResult = cloudinary.Upload(UploadParams, "raw");

            var UploadParams2 = new ImageUploadParams()
            {
                File = new FileDescription(detailImage.FileName, detailImage.InputStream)
            };
            var uploadResult2 = cloudinary.Upload(UploadParams2, "raw");

           
             if (uploadResult == null)
            {
                ViewBag.FileError = "Kapak Resmi yükleme başarısız";
                return View(book);
            }
            else
            {
                book.CoverImage = uploadResult.Url.ToString();
            }
             if (uploadResult2 == null)
            {
                ViewBag.FileError = "Detay Resmi yükleme başarısız";
                return View(book);
            }
            else
            {
                book.DetailImage = uploadResult2.Url.ToString();
            }

            if ((book.Path==null && bookPdf == null)  || (bookPdf != null && bookPdf.ContentLength == 0))
            {
                ViewBag.FileError = "Lütfen bir pdf dosyası yükleyiniz";
                return View(book);
            }
            if (bookPdf!=null && bookPdf.ContentLength > 10 * 1024 * 1024)
            {
                ViewBag.FileError = "Pdf Dosya boyutu 10 MB'dan büyük olamaz";
                return View(book);
            }
            if (bookPdf!=null)
            {
                UploadParams.File = new FileDescription(bookPdf.FileName, bookPdf.InputStream);
                uploadResult = cloudinary.Upload(UploadParams, "raw");
                if (uploadResult == null)
                {
                    ViewBag.FileError = "Pdf yükleme başarısız";
                    return View(book);
                }
                else
                {
                    book.Path = uploadResult.Url.ToString();
                    book.CloudinaryPublicId = uploadResult.PublicId;
                }
            }
           

            DateTime utc = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utc, tzi);
                      
            Book dbBook = new Book();
            dbBook = db.Books.Find(book.Id);
            dbBook.Name = book.Name;
            dbBook.Content = book.Content;
            dbBook.Summary = book.Summary;
            dbBook.Path = book.Path;
            dbBook.ModifyDate = turkeyDateTime;

            db.Entry(dbBook).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(book);
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
