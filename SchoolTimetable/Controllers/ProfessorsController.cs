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
        public async Task<IActionResult> Index()
        {
			string currentUserId = "";
			List<ProfessorViewModel> professorsCollections = await _schoolServices.GetProfessorCollections(currentUserId);

			//check if there are any subjects in database
			bool checkSubjects = await _schoolServices.CheckExistingSubjects();
			ViewData["noSubjects"] = checkSubjects;

			return View(professorsCollections);
        }

        // GET - create a professor
        [HttpGet]
        [Route("/Professors/Create")]
        public async Task<IActionResult> Create()
        {
			//getting a list of all subjects
			ICollection<SchoolSubject> schoolSubjects = await _schoolServices.GetAllSchoolSubjects();

			//check if there are any subjects in database
			bool checkSubjects = await _schoolServices.CheckExistingSubjects();

			if (checkSubjects)
			{
				ViewData["schoolSubjects"] = schoolSubjects;
				string currentUserId = "";
				CreateProfessorViewModel viewModel = new CreateProfessorViewModel { AppUserId = currentUserId };

				return View(viewModel);
			}
			else
			{
				return View("Index");
			}
		}

        // POST - create a professor
        [HttpPost]
        public async Task<IActionResult> Create(CreateProfessorViewModel viewModel)
        {
			//getting a list of all subjects
			ViewData["schoolSubjects"] = await _schoolServices.GetAllSchoolSubjects();

			if (ModelState.IsValid)
			{
				//creating and saving the new professor
				await _schoolServices.AddProfessor(viewModel);
				return RedirectToAction("Index");
			}

			return View(viewModel);
		}

        // POST - assign professors to all classes
        [HttpPost]
        public async Task<IActionResult> Assign()
        {
			string currentUserId = "";
			List<ProfessorViewModel> professorsCollections = await _schoolServices.GetProfessorCollections(currentUserId);

			if (professorsCollections.Count != 0)
			{
				await _schoolServices.AssignAllProfessorsToAllClasses();
			}

			return RedirectToAction("Index");
		}

        // POST - unassign professors from all classes
        [HttpPost]
        public async Task<IActionResult> UnAssignAll()
        {
			string currentUserId = "";
			List<ProfessorViewModel> professorsCollections = await _schoolServices.GetProfessorCollections(currentUserId);

			if (professorsCollections.Count != 0)
			{
				await _schoolServices.UnAssignAllProfessorsFromClasses();
			}

			return RedirectToAction("Index");
		}

        // GET - edit a professor
        [HttpGet]
        [Route("/Professors/Edit/{professorId}")]
        public async Task<IActionResult> Edit(int professorId)
        {
			Professor professor = await _schoolServices.GetProfessor(professorId);

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
		[Route("/Professors/Edit/{professorId}")]
		public async Task<IActionResult> Edit(EditProfessorViewModel viewModel)
        {
			if (ModelState.IsValid)
			{
				await _schoolServices.EditProfessor(viewModel);
				return RedirectToAction("Index");
			}
			return View(viewModel);
		}

        // GET - delete a professor
        [HttpGet]
        [Route("/Professors/Delete/{professorId}")]
        public async Task<IActionResult> Delete(int professorId)
        {
			Professor professor = await _schoolServices.GetProfessor(professorId);

			return View(professor);
		}

        // DELETE a professor
        [HttpPost]
        [Route("/Professors/Delete/{professorId}")]
        public async Task<IActionResult> Delete(Professor viewModel)
        {
			if (viewModel != null)
			{
				await _schoolServices.DeleteProfessor(viewModel);
			}

			return RedirectToAction("Index");
		}
    }
}
