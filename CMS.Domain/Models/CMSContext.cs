using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using CMS.Domain.ViewModels;

namespace CMS.Domain.Models
{
    public class CMSContext : DbContext
    {

        public CMSContext(DbContextOptions<CMSContext> options) : base(options)
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
