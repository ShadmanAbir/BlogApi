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
using CMS.Domain.Helper;

namespace CMS.Core.Services
{
    public class PostService : IPostService
    {
        private UnitOfWork _unitOfWork;
        TimeHelper _timehelper = new TimeHelper();
        public PostService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Post Create(PostViewModel postVM, PostTermViewModel posttermVM,PostStatusViewModel poststatusVM)
        {
            var post = new Post
            {
                PostID = postVM.PostID,
                Title = postVM.Title,
                Content = postVM.Content,
                FeaturedImageUrl = postVM.FeaturedImageUrl,
                Url = postVM.Url,
                CreatedDate = postVM.CreatedDate,
                Author = postVM.Author,
                ModifiedBy = postVM.ModifiedBy,
                ModifiedDate = postVM.ModifiedDate,
                IsDeleted = 0 
            };
            
            post.PostTerms = new List<PostTerm>();
            
            foreach (int termID in posttermVM.TermID)
            {
                post.PostTerms.Add(new PostTerm
                {
                    TermID = termID
                });
            }
            
            post.PostViewStatus = new PostStatus
            {
                ViewCount = 0
            };
            _unitOfWork.PostRepository.Insert(post);
            _unitOfWork.Save();

            return post;
        }

        public Post Update(PostViewModel postVM)
        {
            var post = _unitOfWork.PostRepository.GetById(postVM.PostID);

            post.Title = postVM.Title;
            post.Content = postVM.Content;
            post.FeaturedImageUrl = postVM.FeaturedImageUrl;
            post.Url = postVM.Url;
            post.ModifiedBy = postVM.ModifiedBy;
            post.ModifiedDate = postVM.ModifiedDate;

            _unitOfWork.PostRepository.Update(post);
            _unitOfWork.Save();

            return post;
        }

        public IEnumerable<PostViewModel> GetAllPost()
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate,
                            Terms = GetTermByPost(s.PostID),
                            FeaturedPost = GetFeaturedPost(),
                            Time = _timehelper.CalculateTime(s.CreatedDate)
                        }).AsEnumerable();

            return data;
        }

        public IEnumerable<PostViewModel> GetPostByAuthor(string author)
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        where s.Author == author
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate,
                            Terms = GetTermByPost(s.PostID)
                        }).AsEnumerable();

            return data;
        }

        public PostViewModel GetPostByID(int id)
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        where s.PostID == id
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<PostViewModel> GetPostByTerm(int termID)
        {
            var data = (from p in _unitOfWork.PostRepository.Get()
                        join pt in _unitOfWork.PostTermRepository.Get() on p.PostID equals pt.PostID
                        where pt.TermID == termID
                        select new PostViewModel
                        {
                            PostID = p.PostID,
                            Title = p.Title,
                            Content = p.Content,
                            FeaturedImageUrl = p.FeaturedImageUrl,
                            Url = p.Url,
                            CreatedDate = p.CreatedDate,
                            Author = p.Author,
                            ModifiedBy = p.ModifiedBy,
                            ModifiedDate = p.ModifiedDate,
                            Terms = GetTermByPost(p.PostID)
                        }).AsEnumerable();

            return data;
        }
        public IEnumerable<TermViewModel> GetTermByPost(int postID)
        {
            var data = (from p in _unitOfWork.PostRepository.Get()
                        join pt in _unitOfWork.PostTermRepository.Get() on p.PostID equals pt.PostID
                        join t in _unitOfWork.TermRepository.Get() on pt.TermID equals t.TermID
                        where p.PostID == postID
                        select new TermViewModel
                        {

                            TermID = t.TermID,
                            Type = t.Type,
                            Content = t.Content

                        }).AsEnumerable();

            return data;
        }
        public void Upload(IFormFile file)
        {
            var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "wwwroot/Imagefiles/",
                       file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }


        }

        public IEnumerable<PostViewModel> Search(string content)
        {
            var post = (from s in _unitOfWork.PostRepository.Get()
                        where s.Content.Contains(content) || s.Title.Contains(content) || s.Author.Contains(content) 
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate,
                            Terms = GetTermByPost(s.PostID)
                        }).AsEnumerable();

            return post;
        }

        public PostStatus UpdatePostView(PostStatusViewModel postStatusVM)
        {
            var poststatus =_unitOfWork.PostStatusRepository.GetById(postStatusVM.PostStatusID);
            poststatus.ViewCount = postStatusVM.ViewCount + 1;

            _unitOfWork.PostStatusRepository.Update(poststatus);
            _unitOfWork.Save();

            return poststatus;

        }

        public PostStatusViewModel GetPostView(int postID)
        {
            var postView = (from s in _unitOfWork.PostStatusRepository.Get()
                        where s.PostID == postID
                        select new PostStatusViewModel
                        {
                         PostID=s.PostID,
                         PostStatusID = s.PostStatusID,
                         ViewCount =s.ViewCount
                        }).SingleOrDefault();

            return postView;

        }

        public IEnumerable<PostStatusViewModel> GetPostViewbByAuthor(string author)
        {
            var postView = (from s in _unitOfWork.PostStatusRepository.Get()
                            join pt in _unitOfWork.PostRepository.Get() on s.PostID equals pt.PostID
                            where pt.Author == author
                            select new PostStatusViewModel
                            {
                                PostID = s.PostID,
                                PostStatusID = s.PostStatusID,
                                ViewCount = s.ViewCount
                            }).AsEnumerable();
            return postView;
        }

        public PostViewModel GetLastPost(string user)
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        where s.Author == user orderby s.CreatedDate descending
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).FirstOrDefault();

            return data;


        }

        public PostViewModel GetFeaturedPost()
        {
            var data = (from s in _unitOfWork.PostRepository.Get()
                        join pt in _unitOfWork.PostStatusRepository.Get() on s.PostID equals pt.PostID
                        orderby pt.ViewCount descending
                        select new PostViewModel
                        {
                            PostID = s.PostID,
                            Title = s.Title,
                            Content = s.Content,
                            FeaturedImageUrl = s.FeaturedImageUrl,
                            Url = s.Url,
                            CreatedDate = s.CreatedDate,
                            Author = s.Author,
                            ModifiedBy = s.ModifiedBy,
                            ModifiedDate = s.ModifiedDate
                        }).FirstOrDefault();

            return data;
        }

        public Post Delete(PostViewModel postVM)
        {
            var post = _unitOfWork.PostRepository.GetById(postVM.PostID);

            post.IsDeleted = 1;


            _unitOfWork.PostRepository.Update(post);
            _unitOfWork.Save();

            return post;
        }

        public IEnumerable<PostViewModel> GetReleatedPost(IEnumerable<TermViewModel> Terms)
        {
            var post = new List<PostViewModel>();
            foreach (TermViewModel term in Terms)
            {
                post.AddRange(GetPostByTerm(term.TermID));
            }

            return post;
        }

        public IEnumerable<PostViewModel> GetRank()
        {
            var post = (from pr in _unitOfWork.PostRepository.Get()
                       group pr.Author by new { pr.Author} into g
                       select new PostViewModel
                        {
                            Author =g.Key.Author
                        }).AsEnumerable();


            return post;
        }
    }
}
