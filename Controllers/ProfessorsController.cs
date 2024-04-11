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

        // GET - View all professors
        [HttpGet]
        public IActionResult Index()
        {
            List<ProfessorCollectionsViewModel> professorsCollections = _schoolServices.GetProfessorCollections();

            return View(professorsCollections);
        }

        // GET - create a professor
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["schoolSubjects"] = _schoolServices.GetAllSchoolSubjects();

            return View();
        }

        // POST - create a professor
        [HttpPost]
        public IActionResult Create(ProfessorViewModel viewModel)
        {
            //getting a list of all subjects
            ViewData["schoolSubjects"] = _schoolServices.GetAllSchoolSubjects();

            if (ModelState.IsValid)
            {
                //creating and saving the new professor
                _schoolServices.AddProfessor(viewModel);
            }

            return RedirectToAction("Index");
        }

        // POST - assign professors to all classes
        [HttpPost]
        public IActionResult Assign()
        {
            List<ProfessorCollectionsViewModel> professorsCollections = _schoolServices.GetProfessorCollections();

            if (professorsCollections.Count != 0)
            {
                _schoolServices.AssignAllProfessorsToAllClasses();
            }

            return RedirectToAction("Index");
        }

        // POST - unassign professors from all classes
        [HttpPost]
        public IActionResult UnAssignAll()
        {
            List<ProfessorCollectionsViewModel> professorsCollections = _schoolServices.GetProfessorCollections();

            if (professorsCollections.Count != 0)
            {
                _schoolServices.UnAssignAllProfessorsFromClasses();
            }

            return RedirectToAction("Index");
        }

        // GET - edit a professor
        [HttpGet]
        [Route("Edit/{professorId}")]
        public IActionResult Edit(int professorId)
        {
            Professor professor = _schoolServices.GetProfessor(professorId);
            ViewData["professorSubject"] = _schoolServices.GetSubjectOfProfessor(professorId);

            return View(professor);
        }

        // POST - edit a professor
        [HttpPost]
		[Route("Edit/{professorId}")]
		public IActionResult Edit(Professor viewModel)
        {
            Professor professor = _schoolServices.GetProfessor(viewModel.Id);
            ViewData["professorSubject"] = _schoolServices.GetSubjectOfProfessor(viewModel.Id);

            if (ModelState.IsValid)
            {
                _schoolServices.EditProfessor(viewModel);
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        // GET - delete a professor
        [HttpGet]
        [Route("Delete/{professorId}")]
        public IActionResult Delete(int professorId)
        {
            Professor professor = _schoolServices.GetProfessor(professorId);
            ViewData["professorSubject"] = _schoolServices.GetSubjectOfProfessor(professorId);

            return View(professor);
        }


        // DELETE a professor
        [HttpPost]
        [Route("Delete/{professorId}")]
        public IActionResult Delete(Professor viewModel)
        {
            if (viewModel != null)
            {
				_schoolServices.DeleteProfessor(viewModel);
			}

            return RedirectToAction("Index");
        }
    }
}
