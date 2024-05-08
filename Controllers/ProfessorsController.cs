using Microsoft.AspNetCore.Authorization;
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
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

				//check if there are any subjects in database
				bool checkSubjects = _schoolServices.CheckExistingSubjects();
				ViewData["noSubjects"] = checkSubjects;

				return View(professorsCollections);
			}
            else
            {
                TempData["Error"] = "You must log in to continue";
                return RedirectToAction("Login", "Account");
            }
        }

        // GET - create a professor
        [HttpGet]
        [Route("/Professors/Create")]
        public IActionResult Create()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				//getting a list of all subjects
				ICollection<SchoolSubject> schoolSubjects = _schoolServices.GetAllSchoolSubjects();

				//check if there are any subjects in database
				bool checkSubjects = _schoolServices.CheckExistingSubjects();

				if (checkSubjects)
				{
					ViewData["schoolSubjects"] = schoolSubjects;
					string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
					CreateProfessorViewModel viewModel = new CreateProfessorViewModel { AppUserId = currentUserId };

					return View(viewModel);
				}
				else
				{
					return View("Index");
				}
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - create a professor
        [HttpPost]
        public IActionResult Create(CreateProfessorViewModel viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
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
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - assign professors to all classes
        [HttpPost]
        public IActionResult Assign()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

				if (professorsCollections.Count != 0)
				{
					_schoolServices.AssignAllProfessorsToAllClasses();
				}

				return RedirectToAction("Index");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - unassign professors from all classes
        [HttpPost]
        public IActionResult UnAssignAll()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				List<ProfessorViewModel> professorsCollections = _schoolServices.GetProfessorCollections(currentUserId);

				if (professorsCollections.Count != 0)
				{
					_schoolServices.UnAssignAllProfessorsFromClasses();
				}

				return RedirectToAction("Index");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // GET - edit a professor
        [HttpGet]
		[Authorize]
        [Route("/Professors/Edit/{professorId}")]
        public IActionResult Edit(int professorId)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
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
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - edit a professor
        [HttpPost]
		[Route("/Professors/Edit/{professorId}")]
		public IActionResult Edit(EditProfessorViewModel viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				if (ModelState.IsValid)
				{
					_schoolServices.EditProfessor(viewModel);
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

        // GET - delete a professor
        [HttpGet]
        [Route("/Professors/Delete/{professorId}")]
        public IActionResult Delete(int professorId)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				Professor professor = _schoolServices.GetProfessor(professorId);

				return View(professor);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}


        // DELETE a professor
        [HttpPost]
        [Route("/Professors/Delete/{professorId}")]
        public IActionResult Delete(Professor viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				if (viewModel != null)
				{
					_schoolServices.DeleteProfessor(viewModel);
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
