using System;
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
    public class InterviewController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        // GET: Interview
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var interviews = db.Interviews.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate)
                                    .Select(a => new InterviewIndexViewModel()
                                    {
                                        Id = a.Id,
                                        Title = a.Title,
                                        Summary = a.Summary,
                                        CreateDate = a.CreateDate,
                                        CoverImage = a.CoverImage
                                    }).ToList();
                return View(interviews);
            }
            else
            {
                var interviews = db.Interviews.OrderByDescending(a => a.CreateDate)
                                                   .Select(a => new InterviewIndexViewModel()
                                                   {
                                                       Id = a.Id,
                                                       Title = a.Title,
                                                       Summary = a.Summary,
                                                       CreateDate = a.CreateDate,
                                                       CoverImage = a.CoverImage
                                                   }).ToList();
                return View(interviews);
            }
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
            var commentsWithReplies = db.Comments.AsNoTracking().Where(a => a.Module == Module.Interview && a.ModuleId == id && a.IsOk == true).Select(a => new CommentWithRepliesViewModel
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
            return View(interview);
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
