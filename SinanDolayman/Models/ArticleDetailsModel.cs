using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Models
{
    public class ArticleDetailsModel
    {
        public Article Article { get; set; }
        public List<CommentWithRepliesViewModel> Comments { get; set; } = new List<CommentWithRepliesViewModel>();
    }
}