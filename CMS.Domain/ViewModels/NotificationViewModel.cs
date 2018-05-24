using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.ViewModels
{
    public class NotificationViewModel
    {
        public int NotificationID { get; set; }
        public int PostID { get; set; }
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public int Status { get; set; }
        public DateTime Notificationtime { get; set; }
    }
}
