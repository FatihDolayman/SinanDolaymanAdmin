using System;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Entities;
using PagedList;
using SinanDolayman.Models;


namespace SinanDolayman.Controllers
{
    public class VideoController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

      

        // GET: Video
        public ActionResult Index(string searchTerm, int? page)
        {
            int pageNumber = page ?? 1;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var videos = db.Videos.Where(a => a.Title.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate)
                                    .Select(a => new VideoIndexViewModel()
                                    {
                                        Id = a.Id,
                                        Title = a.Title,
                                        Summary = a.Summary,
                                        CreateDate = a.CreateDate,
                                        CoverImage = a.CoverImage
                                    }).ToList();
                return View(videos.ToPagedList(pageNumber, 10));
            }
            else
            {
                var videos = db.Videos.OrderByDescending(a => a.CreateDate)
                                                   .Select(a => new VideoIndexViewModel()
                                                   {
                                                       Id = a.Id,
                                                       Title = a.Title,
                                                       Summary = a.Summary,
                                                       CreateDate = a.CreateDate,
                                                       CoverImage = a.CoverImage
                                                   }).ToList();
                return View(videos.ToPagedList(pageNumber, 10));
            }
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
            var model = new VideoDetailsModel();
            var commentsWithReplies = db.Comments.AsNoTracking().Where(a => a.Module == Module.Video && a.ModuleId == id && a.IsOk == true).Select(a => new CommentWithRepliesViewModel
            {
                Id = a.Id,
                Content = a.Content,
                Date = a.Date,
                Commenter = a.Commenter,
                LikeCount = a.LikeCount,
                DislikeCount = a.DislikeCount

            }).OrderByDescending(a => a.Date).ToList();
            var replies = db.CommentReplies.AsNoTracking().Where(a => a.IsOk == true).OrderBy(a => a.Date).ToList();
            var repliesCount = 0;
            foreach (var item in commentsWithReplies)
            {
                item.Replies = replies.Where(a => a.CommentId == item.Id).ToList();
                repliesCount = repliesCount + item.Replies.Count();
            }

            model.Comments = commentsWithReplies;
            model.Video = video;
            return View(model);
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
