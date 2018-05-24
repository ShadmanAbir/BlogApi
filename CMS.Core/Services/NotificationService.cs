using System;
using System.Collections.Generic;
using System.Text;
using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using CMS.Core.Interfaces;
using CMS.Domain.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CMS.Core.Services
{
    public class NotificationService : INotificationService
    {
        private UnitOfWork _unitOfWork;

        public NotificationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            _unitOfWork.NotificationRepository.Insert(notification);
            _unitOfWork.Save();

            return notification;
        }

        public IEnumerable<NotificationViewModel> GetNotification(string User)
        {
            var data = (from p in _unitOfWork.PostRepository.Get()
                        join n in _unitOfWork.NotificationRepository.Get() on p.PostID equals n.PostID
                        join c in _unitOfWork.CommentRepository.Get() on n.CommentID equals c.CommentID into comm
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

            int a = (from p in _unitOfWork.PostRepository.Get()
                     join n in _unitOfWork.NotificationRepository.Get() on p.PostID equals n.PostID
                     join c in _unitOfWork.CommentRepository.Get() on n.CommentID equals c.CommentID into comm
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
                notification = _unitOfWork.NotificationRepository.GetById(not.NotificationID);
                notification.Notificationtime = not.Notificationtime;
                notification.PostID = not.PostID;
                notification.UserID = not.UserID;
                notification.Status = 1;
                _unitOfWork.NotificationRepository.Update(notification);
            }
            _unitOfWork.Save();

            return notification;
        }
    }
}
