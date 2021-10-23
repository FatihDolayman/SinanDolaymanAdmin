using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class Video
    {
        public int Id { get; set; }


        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Kapak Resmi")]
        public string CoverImage { get; set; }

        [Display(Name = "Video Yolu")]
        public string VideoPath { get; set; }


        [Display(Name = "Özet")]       
        public string Summary { get; set; }


        [Display(Name = "Oluşturma Tarihi")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }


        [Display(Name = "Son Dünzenlenme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? ModifyDate { get; set; }


        [Display(Name = "Kategori Id")]
        public int? CategoryId { get; set; }


        [Display(Name = "Kategori")]
        public virtual VideoCategory Category { get; set; }


    }
}
