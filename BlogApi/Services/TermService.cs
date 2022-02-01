using BlogApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BlogApi.Models;
using BlogApi.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class TermService : ITermService
    {
        private BlogApiContext _BlogApiContext;

        public TermService(BlogApiContext BlogApiContext)
        {
            _BlogApiContext = BlogApiContext;
        }

        public async Task<Term>Create(TermViewModel termVM)
        {
            var term = new Term
            {
                Type = termVM.Type,
                Content = termVM.Content
            };

            await _BlogApiContext.Term.AddAsync(term);
            await _BlogApiContext.SavechangesAsyncchangesAsync();

            return term;
        }

        public async Task<Term> Update(TermViewModel termVM)
        {
            var term = _BlogApiContext.Term.GetById(termVM.TermID);

            term.TermID = termVM.TermID;
            term.Type = termVM.Type;
            term.Content = termVM.Content;

            _BlogApiContext.Term.Update(term);
            _BlogApiContext.SavechangesAsync();

            return term;
        }

        public async Task<TermViewModel> GetTermByID(int id)
        {
            var data = (from s in _BlogApiContext.Term.Get()
                        where s.TermID == id
                        select new TermViewModel
                        {
                            TermID = s.TermID,
                            Type = s.Type,
                            Content = s.Content
                        }).SingleOrDefault();

            return data;
        }

        public async Task<IEnumerable<TermViewModel>> GetAllTerm()
        {
            var data = (from s in _BlogApiContext.Term.Get()
                        select new TermViewModel
                        {
                            TermID = s.TermID,
                            Type = s.Type,
                            Content = s.Content
                        }).AsEnumerable();

            return data;
        }

        public IEnumerable<TermViewModel> GetTermByPost(int postID)
        {
            var data = (from p in _BlogApiContext.Post.Get()
                        join pt in _BlogApiContext.PostTerm.Get() on p.PostID equals pt.PostID
                        join t in _BlogApiContext.Term.Get() on pt.TermID equals t.TermID
                        where p.PostID == postID
                        select new TermViewModel
                        {

                            TermID = t.TermID,
                            Type = t.Type,
                            Content = t.Content

                        }).AsEnumerable();

            return data;
        }
    }
}
