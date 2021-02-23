using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinanDolaymanAdmin.Models
{
    public class CommetAndReplyListModel
    {
        public List<Comment> Comments { get; set; }
        public List<CommentReply> Replies { get; set; }

    }
}