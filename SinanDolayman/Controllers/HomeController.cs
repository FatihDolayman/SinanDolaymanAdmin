using DAL;
using Entities;
using SinanDolayman.Helpers;
using SinanDolayman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinanDolayman.Controllers
{
    public class HomeController : Controller
    {
        private DolaymanDbContext db = new DolaymanDbContext();

        public ActionResult Index(string searchTerm)
        {
            if (searchTerm == null)
            {
                searchTerm = "";
            }
            SearchModel aramaListeleri = new SearchModel();
            aramaListeleri.ortakModel = new List<CommonModelForIndex>();
            List<CommonModelForIndex> sampleList = new List<CommonModelForIndex>();
            var articles = db.Articles.AsNoTracking().Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).Take(10)
                              .Select(a => new CommonModelForIndex
                              {
                                  Id = a.Id,
                                  TitleOrName = a.Title,
                                  Summary = a.Summary,
                                  DetailImage = a.CoverImage,
                                  CoverImage = a.CoverImage,
                                  CreateDate = a.CreateDate,
                                  Module = Module.Article
                              }).ToList();
            var books = db.Books.AsNoTracking().Where(a => a.Name.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).Take(10)
                            .Select(a => new CommonModelForIndex
                            {
                                Id = a.Id,
                                TitleOrName = a.Name,
                                Summary = a.Summary,
                                CoverImage = a.CoverImage,
                                DetailImage = a.DetailImage,
                                CreateDate = a.CreateDate,
                                Module = Module.Book
                            }).ToList();
            var videos = db.Videos.AsNoTracking().Where(a => a.Title.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).Take(10)
                            .Select(a => new CommonModelForIndex
                            {
                                Id = a.Id,
                                TitleOrName = a.Title,
                                Summary = a.Summary,
                                DetailImage = a.CoverImage,
                                CoverImage = a.CoverImage,
                                CreateDate = a.CreateDate,
                                Module=Module.Video
                            }).ToList();
            sampleList.AddRange(articles);
            sampleList.AddRange(books);
            sampleList.AddRange(videos);
            sampleList=sampleList.OrderByDescending(a => a.CreateDate).Take(10).ToList();
            aramaListeleri.ortakModel = sampleList;

            ViewBag.SearchTerm = searchTerm;
            return View(aramaListeleri);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}