using BlogApi.Interfaces;
using BlogApi.Models;
using BlogApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BlogApi.Services
{
    public class CommentService : ICommentService
    {
        private BlogApiContext _BlogApiContext;
        public CommentService(BlogApiContext BlogApiContext){
            _BlogApiContext = BlogApiContext;
        }

        public IEnumerable<CommentViewModel> GetChildComment()
        {
            var data = (from s in _BlogApiContext.CommentRepository.Get()
                        where s.ParentID != 0 && s.IsApproved == 1
                        select new CommentViewModel
                        {
                            CommentID = s.CommentID,
                            Content = s.Content,
                            PostID = s.PostID,
                            CommentedBy = s.CommentedBy,
                            CommentTime = s.CommentTime,
                            ParentID = s.ParentID

                        }).AsEnumerable();
            return data;
        }

        public IEnumerable<CommentViewModel> CommentsToApprove(string User)
        {
            var data = (from s in _BlogApiContext.CommentRepository.Get()
                        join c in _BlogApiContext.PostRepository.Get() on s.PostID equals c.PostID
                        where c.Author == User && s.IsApproved == 0
                        select new CommentViewModel
                        {
                            CommentID = s.CommentID,
                            Content = s.Content,
                            PostID = s.PostID,
                            CommentedBy = s.CommentedBy,
                            CommentTime = s.CommentTime,
                            
                        }).AsEnumerable();
            return data;
        }

        public Comment Create(CommentViewModel commentVM)
        {
            var comment = new Comment
            {
                CommentID = commentVM.CommentID,
                Content = commentVM.Content,
                PostID = commentVM.PostID,
                CommentedBy=commentVM.CommentedBy,
                CommentTime=DateTime.Now,
                IsApproved=commentVM.IsApproved,
                ParentID = commentVM.ParentID

            };
            _BlogApiContext.CommentRepository.Insert(comment);
            _BlogApiContext.SavechangesAsync();
            
            return comment;
        }

        public IEnumerable<CommentViewModel> GetCommentByAuthor(string User)
        {
            var data = (from s in _BlogApiContext.CommentRepository.Get()
                         where s.CommentedBy == User && s.IsApproved == 1
                         select new CommentViewModel
                         {
                             CommentID = s.CommentID,
                             Content = s.Content,
                             PostID = s.PostID,
                             CommentedBy = s.CommentedBy,
                             CommentTime = s.CommentTime
                         }).AsEnumerable();
            return data;
        }

        public IEnumerable<CommentViewModel> GetCommentByPost(int postID)
        {
            var data = (from s in _BlogApiContext.CommentRepository.Get()
                        where s.PostID == postID && s.ParentID ==0 && s.IsApproved == 1
                        select new CommentViewModel
                        {
                            CommentID = s.CommentID,
                            Content = s.Content,
                            PostID = s.PostID,
                            CommentedBy = s.CommentedBy,
                            CommentTime = s.CommentTime,
                            ParentID = s.ParentID
                        }).AsEnumerable();

            return data;
        }

        public Comment Accept(int id)
        {
            var comment = _BlogApiContext.CommentRepository.GetById(id);
            comment.IsApproved = 1;

            _BlogApiContext.CommentRepository.Update(comment);
            _BlogApiContext.SavechangesAsync();
            return comment;
        }

        public Comment Reject(int id)
        {

            var comment = _BlogApiContext.CommentRepository.GetById(id);
            comment.IsApproved = 2;

            _BlogApiContext.CommentRepository.Update(comment);
            _BlogApiContext.SavechangesAsync();
            return comment;
        }

       
    }
}
