using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;

namespace School_Timetable.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfessorsController(ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
            _schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET - View all professors
        [HttpGet]
        [Route("/Professors")]
        public IActionResult Index()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

            return View(professorsCollections);
        }

        // GET - create a professor
        [HttpGet]
        [Route("/Professors/Create")]
        public IActionResult Create()
        {
            //getting a list of all subjects
            ViewData["schoolSubjects"] = _schoolServices.GetAllSchoolSubjects();

            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            CreateProfessorViewModel viewModel = new CreateProfessorViewModel { AppUserId = currentUserId };

            return View(viewModel);
        }

        // POST - create a professor
        [HttpPost]
        public IActionResult Create(CreateProfessorViewModel viewModel)
        {
            //getting a list of all subjects
            ViewData["schoolSubjects"] = _schoolServices.GetAllSchoolSubjects();

            if (ModelState.IsValid)
            {
				//creating and saving the new professor
				_schoolServices.AddProfessor(viewModel);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // POST - assign professors to all classes
        [HttpPost]
        public IActionResult Assign()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

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
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

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

            EditProfessorViewModel viewModel = new EditProfessorViewModel 
            { 
                Id = professorId,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                ProfessorSubject = professor.ProfessorSubject,
                AssignedHours = professor.AssignedHours
            };

            return View(viewModel);
        }

        // POST - edit a professor
        [HttpPost]
		[Route("Edit/{professorId}")]
		public IActionResult Edit(EditProfessorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _schoolServices.EditProfessor(viewModel);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET - delete a professor
        [HttpGet]
        [Route("Delete/{professorId}")]
        public IActionResult Delete(int professorId)
        {
            Professor professor = _schoolServices.GetProfessor(professorId);

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
