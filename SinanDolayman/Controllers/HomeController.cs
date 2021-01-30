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
            if (searchTerm==null)
            {
                searchTerm = "";
            }
            SearchModel aramaListeleri = new SearchModel();            
            aramaListeleri.Articles = db.Articles.AsNoTracking().Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
            aramaListeleri.Books = db.Books.AsNoTracking().Where(a => a.Name.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
            aramaListeleri.Sounds = db.Sounds.AsNoTracking().Where(a => a.Title.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
            aramaListeleri.Interviews = db.Interviews.AsNoTracking().Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
            aramaListeleri.Videos = db.Videos.AsNoTracking().Where(a => a.Title.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
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