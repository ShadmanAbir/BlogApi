using CMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using CMS.Domain.Repositories;
using System.Linq;

namespace CMS.Core.Services
{
    public class TermService : ITermService
    {
        private UnitOfWork _unitOfWork;

        public TermService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Term Create(TermViewModel termVM)
        {
            var term = new Term
            {
                Type = termVM.Type,
                Content = termVM.Content
            };

            _unitOfWork.TermRepository.Insert(term);
            _unitOfWork.Save();

            return term;
        }

        public Term Update(TermViewModel termVM)
        {
            var term = _unitOfWork.TermRepository.GetById(termVM.TermID);

            term.TermID = termVM.TermID;
            term.Type = termVM.Type;
            term.Content = termVM.Content;

            _unitOfWork.TermRepository.Update(term);
            _unitOfWork.Save();

            return term;
        }

        public TermViewModel GetTermByID(int id)
        {
            var data = (from s in _unitOfWork.TermRepository.Get()
                        where s.TermID == id
                        select new TermViewModel
                        {
                            TermID = s.TermID,
                            Type = s.Type,
                            Content = s.Content
                        }).SingleOrDefault();

            return data;
        }

        public IEnumerable<TermViewModel> GetAllTerm()
        {
            var data = (from s in _unitOfWork.TermRepository.Get()
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







    }
}
