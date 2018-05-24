using CMS.Domain.Models;

namespace CMS.Domain.Repositories
{
    public class UnitOfWork
    {
        private CMSContext _db;

        public UnitOfWork(CMSContext db)
        {
            this._db = db;
        }

        public void Save()
        {
             _db.SaveChanges();
        }

        private IRepository<Post> postRepo;
        public IRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepo == null)
                {
                    this.postRepo = new Repository<Post>(_db);
                }
                return postRepo;
            }
        }

        private IRepository<Term> termRepo;
        public IRepository<Term> TermRepository
        {
            get
            {
                if (this.termRepo == null)
                {
                    this.termRepo = new Repository<Term>(_db);
                }
                return termRepo;
            }
        }

        private IRepository<PostTerm> postTermRepo;
        public IRepository<PostTerm> PostTermRepository
        {
            get
            {
                if (this.postTermRepo == null)
                {
                    this.postTermRepo = new Repository<PostTerm>(_db);
                }
                return postTermRepo;
            }
        }

        private IRepository<Comment> CommentRepo;
        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (this.CommentRepo == null)
                {
                    this.CommentRepo = new Repository<Comment>(_db);
                }
                return CommentRepo;
            }
        }

        private IRepository<PostStatus> PostStatusRepo;
        public IRepository<PostStatus> PostStatusRepository
        {
            get
            {
                if (this.PostStatusRepo == null)
                {
                    this.PostStatusRepo = new Repository<PostStatus>(_db);
                }
                return PostStatusRepo;
            }
        }

        private IRepository<Notification> NotificationRepo;
        public IRepository<Notification> NotificationRepository
        {
            get
            {
                if (this.NotificationRepo == null)
                {
                    this.NotificationRepo = new Repository<Notification>(_db);
                }
                return NotificationRepo;
            }
        }


    }
}
