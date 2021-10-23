using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Helpers
{
    public static class StringHelper
    {
        public static string  SubNullable(this string text,int start, int end)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            if (text.Length<=end-start)
            {
                return text;
            }
           return text.Substring(start, end);
        }
    }
}