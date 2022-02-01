using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.ViewModels
{
    public class PostStatusViewModel
    {
        public int PostStatusID { get; set; }
        public int PostID { get; set; }
        public int ViewCount { get; set; }
    }
}
