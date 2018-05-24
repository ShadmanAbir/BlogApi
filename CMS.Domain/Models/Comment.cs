using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }
        public string CommentedBy { get; set; }
        public int IsApproved { get; set; }
        public int ParentID { get; set; }
    }
}
