using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.ViewModels
{
    public class PostTermViewModel
    {
        public int PostTermID { get; set; }
        public int PostID { get; set; }
        public List<int> TermID { get; set; }
        
    }
}
