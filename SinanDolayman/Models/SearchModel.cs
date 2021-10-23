using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Models
{
    public class SearchModel
    {
        public List<CommonModelForIndex> ortakModel { get; set; }
        public List<AramaKategorisi> Kategoriler { get; set; }

    }
}