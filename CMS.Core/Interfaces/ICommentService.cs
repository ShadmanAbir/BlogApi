using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Interfaces
{
    public interface ICommentService
    {
        Comment Create(CommentViewModel commentVM);
        IEnumerable<CommentViewModel> GetCommentByPost(int postID);
        IEnumerable<CommentViewModel> GetCommentByAuthor(string User);
        IEnumerable<CommentViewModel> CommentsToApprove(string User);
        IEnumerable<CommentViewModel> GetChildComment();
        Comment Accept(int id);
        Comment Reject(int id);
        

    }
}
