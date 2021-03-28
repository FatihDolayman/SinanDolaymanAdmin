using Entities;
using SinanDolayman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolayman.Helpers
{
    public class CommenterHelper
    {
        public static string FindParentCommenterName(List<CommentWithRepliesViewModel> comments,int commentId)
        {
            string commenterName = comments.Where(a => a.Id == commentId).FirstOrDefault().Commenter;
            return commenterName;

        }
    }
}