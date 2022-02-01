using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Models
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}
