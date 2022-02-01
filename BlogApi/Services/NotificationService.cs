using System;
using System.Collections.Generic;
using System.Text;
using BlogApi.Models;
using BlogApi.ViewModels;
using BlogApi.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlogApi.Services
{
    public class NotificationService : INotificationService
    {
        private BlogApiContext _BlogApiContext;

        public NotificationService(BlogApiContext BlogApiContext)
        {
            _BlogApiContext = BlogApiContext;
        }

        public Notification create(CommentViewModel CommentVM)
        {

            var notification = new Notification()
            {
             CommentID = CommentVM.ParentID,
             PostID = CommentVM.PostID,
             Status = 0,
             Notificationtime = CommentVM.CommentTime,
             UserID = CommentVM.CommentedBy
            };

            _BlogApiContext.Notification.Insert(notification);
            _BlogApiContext.SavechangesAsync();

            return notification;
        }

        public IEnumerable<NotificationViewModel> GetNotification(string User)
        {
            var data = (from p in _BlogApiContext.PostRepository.Get()
                        join n in _BlogApiContext.Notification.Get() on p.PostID equals n.PostID
                        join c in _BlogApiContext.CommentRepository.Get() on n.CommentID equals c.CommentID into comm
                        from x in comm.DefaultIfEmpty()
                        where (p.Author == User || x.CommentedBy == User) && n.Status == 0
                        select new NotificationViewModel
                        {
                            CommentID = n.CommentID,
                            Notificationtime = n.Notificationtime,
                            PostID = n.PostID,
                            UserID = n.UserID

                        }).AsEnumerable();
            return data;
        }

        public int NotificationCount(string User)
        {

            int a = (from p in _BlogApiContext.PostRepository.Get()
                     join n in _BlogApiContext.Notification.Get() on p.PostID equals n.PostID
                     join c in _BlogApiContext.CommentRepository.Get() on n.CommentID equals c.CommentID into comm
                     from x in comm.DefaultIfEmpty()
                     where (p.Author == User || x.CommentedBy == User) && n.Status == 0
                     select new NotificationViewModel
                     {
                         CommentID = n.CommentID,
                         Notificationtime = n.Notificationtime,
                         PostID = n.PostID,
                         UserID = n.UserID

                     }).Count();
            return a;
        }

        public Notification Update(IEnumerable<NotificationViewModel> NotificationtVM)
        {
            var notification = new Notification();
            foreach (var not in NotificationtVM)
            {
                notification = _BlogApiContext.Notification.GetById(not.NotificationID);
                notification.Notificationtime = not.Notificationtime;
                notification.PostID = not.PostID;
                notification.UserID = not.UserID;
                notification.Status = 1;
                _BlogApiContext.Notification.Update(notification);
            }
            _BlogApiContext.SavechangesAsync();

            return notification;
        }
    }
}
