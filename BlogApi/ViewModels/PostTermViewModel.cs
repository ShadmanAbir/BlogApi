using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.ViewModels
{
    public class PostTermViewModel
    {
        public int PostTermID { get; set; }
        public int PostID { get; set; }
        public List<int> TermID { get; set; }
        
    }
}
