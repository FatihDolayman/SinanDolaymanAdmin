using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Models
{
    public class SearchModel
    {
        public List<Article> Articles { get; set; }
        public List<Video> Videos { get; set; }
        public List<Interview> Interviews { get; set; }
        public List<Sound> Sounds { get; set; }
        public List<Book> Books { get; set; }

        public List<AramaKategorisi> Kategoriler { get; set; }

    }
}