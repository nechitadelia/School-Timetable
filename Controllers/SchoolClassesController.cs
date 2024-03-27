using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using School_Timetable.Services;
using System.Collections.Generic;

namespace School_Timetable.Controllers
{
    public class SchoolClassesController : Controller
    {
		private readonly ISchoolServices _schoolServices;

		public SchoolClassesController(ISchoolServices schoolServices)
        {
			_schoolServices = schoolServices;
		}

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all classes
            ICollection<SchoolClass> schoolClasses = _schoolServices.GetAllClasses();

            ViewData["classesSubjects"] = _schoolServices.GetSubjectsForAllClasses(schoolClasses);
			ViewData["classProfessors"] = _schoolServices.GetProfessorsForAllClasses(schoolClasses);

			return View(schoolClasses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SchoolClassViewModel viewModel)
        {
            ViewData["availableLetter"] = _schoolServices.GetAvailableLetter(viewModel);

			//add class to database
			_schoolServices.AddClass(viewModel);

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(SchoolClassViewModel viewModel)
        {
			_schoolServices.DeleteClass(viewModel);

			return View();
		}
	}
}
