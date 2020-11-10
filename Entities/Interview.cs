using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entities
{
    public class Interview
    {
        public int Id { get; set; }


        [Display(Name="Başlık")]
        public string Title { get; set; }


        [Display(Name = "İçerik")]
        [AllowHtml]

        public string Content { get; set; }


        [Display(Name = "Oluşturma Tarihi")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }


        [Display(Name = "Son Düzenleme Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }


       
        
    }
}
