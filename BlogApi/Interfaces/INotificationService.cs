using System;
using System.Collections.Generic;
using System.Text;
using BlogApi.Models;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BlogApi.Interfaces
{
    public interface INotificationService
    {
        Notification create(CommentViewModel CommentVM);
        Notification Update(IEnumerable<NotificationViewModel> NotificationtVM);
        int NotificationCount(string User);
        IEnumerable<NotificationViewModel> GetNotification(string User);
    }
}
