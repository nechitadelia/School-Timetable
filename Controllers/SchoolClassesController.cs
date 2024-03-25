using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;

namespace School_Timetable.Controllers
{
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public SchoolClassesController(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all classes from the database
            ICollection<SchoolClass> schoolClasses = _schoolClassRepository.GetAllClasses();

            List<List<SchoolSubject>> classesSubjects = new List<List<SchoolSubject>>();

            foreach (var schoolClass in schoolClasses)
            {
                classesSubjects.Add(_schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy));
            }

            ViewData["classesSubjects"] = classesSubjects;

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
            //displaying the next available letter in the view model
            char availableLetter = _schoolClassRepository.GetAvailableLetter(viewModel.YearOfStudy);

            ViewData["availableLetter"] = availableLetter;

            //add class to database
            _schoolClassRepository.AddClass(viewModel.YearOfStudy);

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
			_schoolClassRepository.DeleteClass(viewModel.YearOfStudy);

			return View();
		}
	}
}
