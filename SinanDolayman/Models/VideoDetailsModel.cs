using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Models
{
    public class VideoDetailsModel
    {
        public Video Video { get; set; }
        public List<CommentWithRepliesViewModel> Comments { get; set; } = new List<CommentWithRepliesViewModel>();
    }
}