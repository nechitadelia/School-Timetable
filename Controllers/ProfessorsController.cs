using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using School_Timetable.Services;

namespace School_Timetable.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ISchoolServices _schoolServices;

		public ProfessorsController(ISchoolServices schoolServices)
        {
            _schoolServices = schoolServices;
		}

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all professors
            ICollection<Professor> professors = _schoolServices.GetAllProfessors();
            
            //saving the data of professors, so it can be sent to the View
            ViewData["professorSubjects"] = _schoolServices.GetAllProfessorsSubjects(professors);
            ViewData["unassignedHours"] = _schoolServices.GetAllProfessorsUnassignedHours(professors);
            ViewData["professorClasses"] = _schoolServices.GetAllProfessorsClasses(professors);

            return View(professors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["schoolSubjects"] = _schoolServices.GetAllSchoolSubjects();

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProfessorViewModel viewModel)
        {
            //getting a list of all subjects
            ICollection<SchoolSubject> schoolSubjects = _schoolServices.GetAllSchoolSubjects();

			//creating and saving the new professor
			_schoolServices.AddProfessor(viewModel, schoolSubjects);

            ViewData["schoolSubjects"] = schoolSubjects;

            return View();
        }

        [HttpGet, ActionName("Edit")]
        public IActionResult Edit(int professorId)
        {
            Professor professor = _schoolServices.GetProfessor(professorId);
			ViewData["professorSubject"] = _schoolServices.GetProfessorSubject(professorId);

			return View(professor);
        }

        [HttpPost]
        public IActionResult Edit(Professor viewModel)
        {
			_schoolServices.EditProfessor(viewModel);

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int professorId)
        {
			Professor professor = _schoolServices.GetProfessor(professorId);
			_schoolServices.DeleteProfessor(professor);

            return RedirectToAction("Index");
        }
    }
}
