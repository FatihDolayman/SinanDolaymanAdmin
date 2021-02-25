﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entities;
using SinanDolayman.Models;

namespace SinanDolayman.Controllers
{
    public class BookController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Book
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var books = db.Books.Where(a => a.Name.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate)
                                    .Select(a => new BookIndexViewModel()
                                    {
                                        Id = a.Id,
                                        Name = a.Name,
                                        Summary = a.Summary,
                                        CreateDate = a.CreateDate,
                                        CoverImage = a.CoverImage
                                    }).ToList();
                return View(books);
            }
            else
            {
                var books = db.Books.OrderByDescending(a => a.CreateDate)
                                                   .Select(a => new BookIndexViewModel()
                                                   {
                                                       Id = a.Id,
                                                       Name = a.Name,
                                                       Summary = a.Summary,
                                                       CreateDate = a.CreateDate,
                                                       CoverImage = a.CoverImage
                                                   }).ToList();
                return View(books);
            }
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
            var commentsWithReplies = db.Comments.AsNoTracking().Where(a => a.Module == Module.Book && a.ModuleId == id && a.IsOk == true).Select(a => new CommentWithRepliesViewModel
            {
                Id = a.Id,
                Content = a.Content,
                Date = a.Date,
                Commenter = a.Commenter,
                LikeCount = a.LikeCount,
                DislikeCount = a.DislikeCount

            }).OrderByDescending(a => a.Date).ToList();
            var replies = db.CommentReplies.AsNoTracking().Where(a => a.IsOk == true).OrderBy(a => a.Date).ToList();
            foreach (var item in commentsWithReplies)
            {
                item.Replies = replies.Where(a => a.CommentId == item.Id).ToList();
            }

            ViewBag.Comments = commentsWithReplies;
            return View(book);
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
