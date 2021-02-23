using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinanDolayman.Models
{
    public class CommentWithRepliesViewModel
    {
        public int Id { get; set; }


        [Display(Name = "İçerik")]
        public string Content { get; set; }


        [Display(Name = "Onaylı mı?")]
        public bool IsOk { get; set; }


        [Display(Name = "Tarih")]

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Display(Name = "Yorum Sahibi")]

        public string Commenter { get; set; }

        [Display(Name = "Beğeni Sayısı")]
        public int LikeCount { get; set; }

        [Display(Name = "Beğenmeme Sayısı")]
        public int DislikeCount { get; set; }


        [Display(Name = "Modül")]
        public int ModuleId { get; set; }

        public Module Module { get; set; }

        public List<CommentReply> Replies { get; set; }
    }
}