using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class Article
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name ="Yazar")]
        public string Author { get; set; }

        [Display(Name = "Kapak Resmi")]
        public string CoverImage { get; set; }

        [Display(Name = "Özet")]
        [AllowHtml]
        public string Summary { get; set; }

        [Display(Name = "İçerik")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name ="Oluştuma Tarih")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Son Düzenleme Tarih")]
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }


     


    }
}
