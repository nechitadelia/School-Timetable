using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace School_Timetable.Controllers
{
    public class SchoolSubjectsController : Controller
    {
		private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolSubjectsController(ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
			_schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

		// GET - View all subjects
		[HttpGet]
        [Route("/SchoolSubjects")]
        public async Task<IActionResult> Index()
        {
			if(User.Identity.IsAuthenticated && User.IsInRole("User"))
			{
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				List<SchoolSubjectViewModel> subjects = await _schoolServices.GetSubjectsCollections(currentUserId);

				return View(subjects);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

		// GET - create a subject
		[HttpGet]
        [Route("/SchoolSubject/Create")]
        public IActionResult Create()
        {
			if(User.Identity.IsAuthenticated && User.IsInRole("User"))
			{
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				CreateSchoolSubjectViewModel viewModel = new CreateSchoolSubjectViewModel { AppUserId = currentUserId };

				return View(viewModel);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

		// POST - create a subject
		[HttpPost]
        [Route("/SchoolSubject/Create")]
        public async Task<IActionResult> Create(CreateSchoolSubjectViewModel viewModel)
        {
			if(User.Identity.IsAuthenticated && User.IsInRole("User"))
			{
				if (ModelState.IsValid)
				{
					//creating and saving the new subject
					await _schoolServices.AddSubject(viewModel);
					return RedirectToAction("Index");
				}

				return View(viewModel);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

		// GET - delete a subject
		[HttpGet]
		[Route("/SchoolSubject/Delete/{subjectId}")]
        public async Task<IActionResult> Delete(int subjectId)
        {
			if(User.Identity.IsAuthenticated && User.IsInRole("User"))
			{
				SchoolSubject subject = await _schoolServices.GetSchoolSubject(subjectId);

				return View(subject);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

		// DELETE - delete a subject
		[HttpPost]
		[Route("/SchoolSubject/Delete/{professorId}")]
		public async Task<IActionResult> Delete(SchoolSubject viewModel)
		{
			if(User.Identity.IsAuthenticated && User.IsInRole("User"))
			{
				if (viewModel != null)
				{
					bool result = await _schoolServices.DeleteSchoolSubject(viewModel);
					if (result)
					{
						return RedirectToAction("Index");
					}
					else
					{
						TempData["Error"] = "The subject has professors assigned to it. You need to delete those professors first.";
						return View(viewModel);
					}
				}

				return RedirectToAction("Index");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
