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
		private readonly IClassProfessorRepository _classProfessorRepository;

		public SchoolClassesController(ISchoolClassRepository schoolClassRepository, IClassProfessorRepository classProfessorRepository)
        {
            _schoolClassRepository = schoolClassRepository;
			_classProfessorRepository = classProfessorRepository;
		}

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all classes from the database
            ICollection<SchoolClass> schoolClasses = _schoolClassRepository.GetAllClasses();

            //getting the list of all subjects for all classes
            List<List<SchoolSubject>> classesSubjects = new List<List<SchoolSubject>>();
			List<List<string>> classProfessors = new List<List<string>>();

			foreach (SchoolClass schoolClass in schoolClasses)
            {
                List<SchoolSubject> subjects = _schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy); //get the list of all subjects for one class
				classesSubjects.Add(subjects);
                classProfessors.Add(_classProfessorRepository.GetProfessorsOfAClass(schoolClass, subjects)); //get the list of all professors of one class
            }

            ViewData["classesSubjects"] = classesSubjects;
            ViewData["classProfessors"] = classProfessors;

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
            SchoolClass schoolClass = _schoolClassRepository.GetClassFromViewModel(viewModel);
			_classProfessorRepository.UnassignAClass(schoolClass);

			_schoolClassRepository.DeleteClass(viewModel.YearOfStudy);

			return View();
		}
	}
}
