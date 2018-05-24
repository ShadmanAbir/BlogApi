using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{

    public class Term
    {
        public int TermID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }

        public ICollection<PostTerm> PostTerms { get; set; }
    }
}
