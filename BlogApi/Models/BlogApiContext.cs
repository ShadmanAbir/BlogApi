using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using BlogApi.ViewModels;

namespace BlogApi.Models
{
    public class BlogApiContext : DbContext
    {

        public BlogApiContext(DbContextOptions<BlogApiContext> options) : base(options)
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<PostTerm> PostTerm { get; set; }
        public DbSet<PostStatus> PostStatus { get; set; }
        public DbSet<Notification> Notification { get; set; }
    }
}
