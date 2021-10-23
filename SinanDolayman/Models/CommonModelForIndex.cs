using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinanDolayman.Helpers;

namespace SinanDolayman.Models
{

    public class CommonModelForIndex
    {
        public int Id { get; set; }
        public string TitleOrName  { get; set; }
        public string CoverImage { get; set; }
        public string DetailImage { get; set; }
        public string Summary { get; set; }
        public DateTime CreateDate { get; set; }
        public Entities.Module Module { get; set; }

        public string GenerateSlug()
        {
            string phrase = string.Format("{0}-{1}-{2}", Id, TitleOrName, CreateDate.ToShortDateString());

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