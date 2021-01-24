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

        public ActionResult Index(string searchTerm, int? Kategoriler)
        {
            SearchModel aramaListeleri = new SearchModel();
            aramaListeleri.Kategoriler = new List<AramaKategorisi>();
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Tüm Kategorilerde",Value=0 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Makalelerde", Value = 1});
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Kitaplarda", Value = 2 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Videolarda", Value = 3 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Seslerde", Value = 4 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Röportajlarda", Value = 5 });

            ViewBag.Kategoriler = new SelectList(aramaListeleri.Kategoriler, "Value", "Kategori", Kategoriler);


            if (!String.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;              

                switch (Kategoriler)
                {
                    case 0:
                        aramaListeleri.Articles = db.Articles.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        aramaListeleri.Books = db.Books.Where(a => a.Name.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        aramaListeleri.Sounds = db.Sounds.Where(a => a.Title.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        aramaListeleri.Interviews = db.Interviews.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        aramaListeleri.Videos = db.Videos.Where(a => a.Title.Contains(searchTerm) ||a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;
                    case 1:
                        aramaListeleri.Articles = db.Articles.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;
                    case 2:
                        aramaListeleri.Books = db.Books.Where(a => a.Name.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;
                    case 3:
                        aramaListeleri.Videos = db.Videos.Where(a => a.Title.Contains(searchTerm) || a.Summary.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;
                    case 4:
                        aramaListeleri.Sounds = db.Sounds.Where(a => a.Title.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;
                    case 5:
                        aramaListeleri.Interviews = db.Interviews.Where(a => a.Title.Contains(searchTerm) || a.Content.Contains(searchTerm)).OrderByDescending(a => a.CreateDate).ToList();
                        break;                    

                }

                ViewBag.SearchTerm = searchTerm;
                return View(aramaListeleri);
            }
            else
            {
                aramaListeleri.Articles = db.Articles.OrderByDescending(a => a.CreateDate).Take(5).ToList();
                aramaListeleri.Sounds = db.Sounds.OrderByDescending(a => a.CreateDate).Take(5).ToList();
                aramaListeleri.Videos = db.Videos.OrderByDescending(a => a.CreateDate).Take(5).ToList();
                aramaListeleri.Books = db.Books.OrderByDescending(a => a.CreateDate).Take(5).ToList();
                aramaListeleri.Interviews = db.Interviews.OrderByDescending(a => a.CreateDate).Take(5).ToList();

                return View(aramaListeleri);
            }

        }


        [ChildActionOnly]
        public ActionResult SearchPartial(string searchTerm)
        {
            SearchModel aramaListeleri = new SearchModel();
            aramaListeleri.Kategoriler = new List<AramaKategorisi>();
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Tüm Kategorilerde", Value = 0 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Makalelerde", Value = 1 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Kitaplarda", Value = 2 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Videolarda", Value = 3 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Seslerde", Value = 4 });
            aramaListeleri.Kategoriler.Add(new AramaKategorisi { Kategori = "Röportajlarda", Value = 5 });

            ViewBag.Kategoriler = new SelectList(aramaListeleri.Kategoriler, "Value", "Kategori");

            ViewBag.Search = searchTerm;
            return PartialView(aramaListeleri);
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