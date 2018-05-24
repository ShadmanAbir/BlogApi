using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class PostStatus
    {
        public int PostStatusID { get; set; }
        public int PostID { get; set; }
        public int ViewCount { get; set; }

        public virtual Post Post { get; set; }
    }
}
