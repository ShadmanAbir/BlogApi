using BlogApi.Models;
using BlogApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Interfaces
{
    public interface ITermService
    {
        Task<Term> Create(TermViewModel TermVM);
        Task<Term> Update(TermViewModel TermVM);
        Task<TermViewModel> GetTermByID(int id);
        Task<IEnumerable<TermViewModel>> GetAllTerm();
        
    }
}
