using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Models
{
    public class BaseEntity
    {
        //public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
