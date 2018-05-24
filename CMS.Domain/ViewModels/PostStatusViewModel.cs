using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.ViewModels
{
    public class PostStatusViewModel
    {
        public int PostStatusID { get; set; }
        public int PostID { get; set; }
        public int ViewCount { get; set; }
    }
}
