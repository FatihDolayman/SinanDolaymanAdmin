using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Helpers
{
    public static class ReplaceChars
    {
        public static string ReplaceTurkishChars(string metin)
        {
            if (String.IsNullOrEmpty(metin))
            {
                return "";
            }
            metin = metin.ToLower();
            metin = metin.Replace('ö','o');
            metin = metin.Replace('ç', 'c');
            metin = metin.Replace('ş', 's');
            metin = metin.Replace('ü', 'u');
          
            return metin;
        }
    }
}