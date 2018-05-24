using CMS.Domain.Models;
using CMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Interfaces
{
    public interface ITermService
    {
        Term Create(TermViewModel TermVM);
        Term Update(TermViewModel TermVM);
        TermViewModel GetTermByID(int id);
        IEnumerable<TermViewModel> GetAllTerm();
        
    }
}
