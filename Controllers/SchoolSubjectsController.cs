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
        public IActionResult Index()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<SchoolSubjectViewModel> subjects = _schoolServices.GetSubjectsCollections(currentUserId);

			return View(subjects);
        }

		// GET - create a subject
		[HttpGet]
        [Route("/SchoolSubject/Create")]
        public IActionResult Create()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            CreateSchoolSubjectViewModel viewModel = new CreateSchoolSubjectViewModel { AppUserId = currentUserId };

            return View(viewModel);
        }

		// POST - create a subject
		[HttpPost]
        [Route("/SchoolSubject/Create")]
        public IActionResult Create(CreateSchoolSubjectViewModel viewModel)
        {
			if (ModelState.IsValid)
			{
				//creating and saving the new subject
				_schoolServices.AddSubject(viewModel);
				return RedirectToAction("Index");
			}

			return View(viewModel);
		}

		// GET - delete a subject
		[HttpGet]
		[Route("/SchoolSubject/Delete/{subjectId}")]
        public IActionResult Delete(int subjectId)
        {
            SchoolSubject subject = _schoolServices.GetSchoolSubject(subjectId);

			return View(subject);
		}

		// DELETE - delete a subject
		[HttpPost]
		[Route("/SchoolSubject/Delete/{professorId}")]
		public IActionResult Delete(SchoolSubject viewModel)
		{
			if (viewModel != null)
			{
				bool result = _schoolServices.DeleteSchoolSubject(viewModel);
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
	}
}
