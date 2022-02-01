using BlogApi.Models;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Interfaces
{
    public interface IPostService
    {
        Task<Post> Create(PostViewModel postVM, PostTermViewModel posttermVM, PostStatusViewModel poststatusVM);
        Task<Post> Update(PostViewModel postVM);
        Task<Post> Delete(PostViewModel postVM);
        Task<PostViewModel> GetPostByID(int id);
        Task<PostViewModel> GetLastPost(string user);
        Task<PostViewModel> GetFeaturedPost();
        Task<IEnumerable<PostViewModel>> GetPostByAuthor(string author);
        Task<IEnumerable<PostViewModel>> GetPostByTerm(int termID);
        Task<IEnumerable<PostViewModel>> GetAllPost();
        Task<IEnumerable<PostViewModel>> GetTermByPost(int postID);
        Task<IEnumerable<PostViewModel>> Search(string content);
        Task<IEnumerable<PostViewModel>> GetReleatedPost(IEnumerable<TermViewModel> Terms);
        Task<IEnumerable<PostViewModel>> GetRank();
        Task<PostStatus> UpdatePostView(PostStatusViewModel postStatusVM);
        Task<PostStatusViewModel> GetPostView(int postID);
        IEnumerable<PostStatusViewModel> GetPostViewbByAuthor(string author);
        void Upload(IFormFile file);
        
    }
}
