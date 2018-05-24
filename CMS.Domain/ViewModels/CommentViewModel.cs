using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Domain.ViewModels
{
    public class CommentViewModel
    {
        [Key]
        public int CommentID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int PostID { get; set; }
        public DateTime CommentTime { get; set; }
        public string CommentedBy { get; set; }
        public int IsApproved { get; set; }
        public int ParentID { get; set; }
    }
}
