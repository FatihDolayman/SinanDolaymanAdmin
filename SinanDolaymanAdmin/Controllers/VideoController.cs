﻿using DAL;
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,CoverImage,Summary,VideoPath")] Video video, HttpPostedFileBase image)
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
                        return View(video);
                    }

                    fileName = Guid.NewGuid() + ".png";
                    image.SaveAs(Path.Combine(Server.MapPath("/SiteResimleri/"), fileName));

                    video.CoverImage = "/SiteResimleri/" + fileName;
                }

                video.CreateDate = DateTime.Now;
                video.ModifyDate = DateTime.Now;
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,VideoPath,Summary,CategoryId")] Video video)
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
