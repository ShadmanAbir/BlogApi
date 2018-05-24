using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Interfaces
{
    public interface IPostService
    {
        Post Create(PostViewModel postVM, PostTermViewModel posttermVM, PostStatusViewModel poststatusVM);
        Post Update(PostViewModel postVM);
        Post Delete(PostViewModel postVM);
        PostViewModel GetPostByID(int id);
        PostViewModel GetLastPost(string user);
        PostViewModel GetFeaturedPost();
        IEnumerable<PostViewModel> GetPostByAuthor(string author);
        IEnumerable<PostViewModel> GetPostByTerm(int termID);
        IEnumerable<PostViewModel> GetAllPost();
        IEnumerable<TermViewModel> GetTermByPost(int postID);
        IEnumerable<PostViewModel> Search(string content);
        IEnumerable<PostViewModel> GetReleatedPost(IEnumerable<TermViewModel> Terms);
        IEnumerable<PostViewModel> GetRank();
        PostStatus UpdatePostView(PostStatusViewModel postStatusVM);
        PostStatusViewModel GetPostView(int postID);
        IEnumerable<PostStatusViewModel> GetPostViewbByAuthor(string author);
        void Upload(IFormFile file);
        
    }
}
