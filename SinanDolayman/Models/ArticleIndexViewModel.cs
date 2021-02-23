using SinanDolayman.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SinanDolayman.Models
{
    public class ArticleIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }     

        [Display(Name = "Kapak Resmi")]
        public string CoverImage { get; set; }

        [Display(Name = "Özet")]
        public string Summary { get; set; }
       

        [Display(Name = "Oluştuma Tarih")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}-{2}", Id, Title, CreateDate.ToShortDateString());

            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = StringDuzenleyici.AdresDuzenle(str);
            
            return str;
        }

        private string RemoveAccent(string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

    }
}