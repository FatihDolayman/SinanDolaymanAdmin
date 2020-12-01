using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class Sound
    {
        public int Id { get; set; }


        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Kapak Resmi")]
        public string CoverImage { get; set; }

        [Display(Name = "Özet")]
        [AllowHtml]
        public string Summary { get; set; }

        [Display(Name = "Ses Yolu")]
        public string Path { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "Son Düzenlenme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }


        [Display(Name = "Kategori Id")]
        public int CategoryId { get; set; }


        [Display(Name = "Kategori")]
        public virtual SoundCategory Category { get; set; }


       

    }
}
