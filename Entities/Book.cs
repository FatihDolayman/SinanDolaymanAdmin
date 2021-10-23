using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class Book
    {
        public int Id { get; set; }


        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Yazar")]
        public string Author { get; set; }

        [Display(Name = "Özet")]       
        public string Summary { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }


        [Display(Name = "Kapak Resmi")]
        public string CoverImage { get; set; }

        [Display(Name = "Kapak Resmi")]
        public string DetailImage { get; set; }

        [Display(Name = "İçerik")]
        [AllowHtml]
        public string Content { get; set; }


        [Display(Name = "PDF")]
        public string Path { get; set; }
        public string CloudinaryPublicId { get; set; }



        [Display(Name = "Oluşturma Tarihi")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }


        [Display(Name = "Son Düzenlenme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? ModifyDate { get; set; }


    }
}
