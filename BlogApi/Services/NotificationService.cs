﻿using System;
using System.Collections.Generic;
using System.Text;
using BlogApi.Models;
using BlogApi.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlogApi.Services
{
    public class NotificationService
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

            _BlogApiContext.Notification.AddAsync(notification);
            _BlogApiContext.SaveChangesAsync();

            return notification;
        }

        public IEnumerable<NotificationViewModel> GetNotification(string User)
        {
            var data = (from p in _BlogApiContext.Post
                        join n in _BlogApiContext.Notification on p.PostID equals n.PostID
                        join c in _BlogApiContext.Comment on n.CommentID equals c.CommentID into comm
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

            int a = (from p in _BlogApiContext.Post
                     join n in _BlogApiContext.Notification on p.PostID equals n.PostID
                     join c in _BlogApiContext.Comment on n.CommentID equals c.CommentID into comm
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

        public async Task<Notification> Update(IEnumerable<NotificationViewModel> NotificationtVM)
        {
            var notification = new Notification();
            foreach (var not in NotificationtVM)
            {
                notification = await _BlogApiContext.Notification.FindAsync(not.NotificationID);
                notification.Notificationtime = not.Notificationtime;
                notification.PostID = not.PostID;
                notification.UserID = not.UserID;
                notification.Status = 1;
                _BlogApiContext.Notification.Update(notification);
            }
            await _BlogApiContext.SaveChangesAsync();

            return notification;
        }
    }
}
