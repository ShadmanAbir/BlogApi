using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int PostID { get; set; }
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public int Status { get; set; }
        public DateTime Notificationtime { get; set; }
    }
}
