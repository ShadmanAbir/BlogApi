using BlogApi.Models;
using BlogApi.ViewModels;
using BlogApi.Helper;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class PostService 
    {
        private BlogApiContext _BlogApiContext;
        TimeHelper _timehelper = new TimeHelper();
        public PostService(BlogApiContext BlogApiContext)
        {
            _BlogApiContext = BlogApiContext;
        }

        public async Task<Post> Create(PostViewModel postVM, PostTermViewModel posttermVM,PostStatusViewModel poststatusVM)
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
            await _BlogApiContext.Post.AddAsync(post);
            await _BlogApiContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> UpdateAsync(PostViewModel postVM)
        {
            var post = await _BlogApiContext.Post.SingleOrDefaultAsync(d => d.PostID == postVM.PostID);

            post.Title = postVM.Title;
            post.Content = postVM.Content;
            post.FeaturedImageUrl = postVM.FeaturedImageUrl;
            post.Url = postVM.Url;
            post.ModifiedBy = postVM.ModifiedBy;
            post.ModifiedDate = postVM.ModifiedDate;

            _BlogApiContext.Post.Update(post);
            await _BlogApiContext.SaveChangesAsync();

            return post;
        }

        public IEnumerable<PostViewModel> GetAllPost()
        {
            var data = (from s in _BlogApiContext.Post
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
            var data = (from s in _BlogApiContext.Post
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
            var data = (from s in _BlogApiContext.Post
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
            var data = (from p in _BlogApiContext.Post
                        join pt in _BlogApiContext.PostTerm on p.PostID equals pt.PostID
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
            var data = (from p in _BlogApiContext.Post
                        join pt in _BlogApiContext.PostTerm on p.PostID equals pt.PostID
                        join t in _BlogApiContext.Term on pt.TermID equals t.TermID
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
            var post = (from s in _BlogApiContext.Post
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

        public async Task<PostStatus> UpdatePostViewAsync(PostStatusViewModel postStatusVM)
        {
            var poststatus =await _BlogApiContext.PostStatus.SingleOrDefaultAsync(d =>d.PostStatusID == postStatusVM.PostStatusID);
            poststatus.ViewCount = postStatusVM.ViewCount + 1;

            _BlogApiContext.PostStatus.Update(poststatus);
            await _BlogApiContext.SaveChangesAsync();

            return poststatus;

        }

        public PostStatusViewModel GetPostView(int postID)
        {
            var postView = (from s in _BlogApiContext.PostStatus
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
            var postView = (from s in _BlogApiContext.PostStatus
                            join pt in _BlogApiContext.Post on s.PostID equals pt.PostID
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
            var data = (from s in _BlogApiContext.Post
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
            var data = (from s in _BlogApiContext.Post
                        join pt in _BlogApiContext.PostStatus on s.PostID equals pt.PostID
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

        public async Task<Post> Delete(PostViewModel postVM)
        {
            var post =await _BlogApiContext.Post.SingleOrDefaultAsync(d => d.PostID == postVM.PostID);

            post.IsDeleted = 1;


            _BlogApiContext.Post.Update(post);
            await _BlogApiContext.SaveChangesAsync();

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
            var post = (from pr in _BlogApiContext.Post
                       group pr.Author by new { pr.Author} into g
                       select new PostViewModel
                        {
                            Author =g.Key.Author
                        }).AsEnumerable();


            return post;
        }
    }
}
