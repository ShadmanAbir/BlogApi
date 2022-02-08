using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApi.Models;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using BlogApi.Services;


namespace BlogApi.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TermController : Controller
    {
        private readonly TermService termService;
        
        public TermController(TermService _termService)
        {
            termService = _termService;           
        }


        public ActionResult Index()
        {
            var item = termService.GetAllTerm();
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TermViewModel termVM)
        {

            termService.Create(termVM);
            ViewBag.Message = "Term added succesfully";

            return View();
        }

        public ActionResult Edit(int id)
        {
            var term = termService.GetTermByID(id);

            return View(term);
        }

        [HttpPost]
        public ActionResult Edit(TermViewModel termVM)
        {
            
            termService.Update(termVM);

            return View();
        }
    }
}
