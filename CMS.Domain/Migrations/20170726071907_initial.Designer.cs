using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CMS.Domain.Models;

namespace CMS.Domain.Migrations
{
    [DbContext(typeof(CMSContext))]
    [Migration("20170726071907_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("PostID");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CMS.Domain.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FeaturedImageUrl");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("PostID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("CMS.Domain.Models.PostTerm", b =>
                {
                    b.Property<int>("PostTermID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostID");

                    b.Property<int>("TermID");

                    b.HasKey("PostTermID");

                    b.HasIndex("PostID");

                    b.HasIndex("TermID");

                    b.ToTable("PostTerm");
                });

            modelBuilder.Entity("CMS.Domain.Models.Term", b =>
                {
                    b.Property<int>("TermID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Type");

                    b.HasKey("TermID");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.HasOne("CMS.Domain.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMS.Domain.Models.PostTerm", b =>
                {
                    b.HasOne("CMS.Domain.Models.Post", "Post")
                        .WithMany("PostTerms")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMS.Domain.Models.Term", "Term")
                        .WithMany("PostTerms")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
