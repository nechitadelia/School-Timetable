using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
using System.Collections.Generic;

namespace School_Timetable.Controllers
{
    public class SchoolClassesController : Controller
    {
		private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolClassesController(ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
			_schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET - View all classes
        [HttpGet]
        [Route("/SchoolClasses")]
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				//getting a list of all classes
				SchoolClassCollectionsViewModel classCollections = await _schoolServices.GetClassCollections();

				return View(classCollections);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // GET - Create a class
        [HttpGet]
        [Route("/SchoolClasses/Create")]
        public async Task<IActionResult> Create()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				CreateSchoolClassViewModel viewModel = new CreateSchoolClassViewModel
				{
					AppUserId = currentUserId,
					AllAvailableLetters = await _schoolServices.GetAllAvailableLetters()
				};

				return View(viewModel);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - Create a class
        [HttpPost]
        public async Task<IActionResult> Create(CreateSchoolClassViewModel viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				await _schoolServices.AddClass(viewModel);

				return RedirectToAction("Create");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // GET - Graduate all classes
        [HttpGet]
        [Route("/SchoolClasses/GraduateClasses")]
        public async Task<IActionResult> GraduateClasses()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				//getting a list of all classes
				SchoolClassCollectionsViewModel classCollections = await _schoolServices.GetClassCollections();

				return View(classCollections);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // POST - Graduate all classes
        [HttpPost]
        public async Task<IActionResult> GraduateClasses(SchoolClassCollectionsViewModel viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				await _schoolServices.GraduateClasses();
				return RedirectToAction("Index");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // GET - Delete one class
        [HttpGet]
        [Route("/SchoolClasses/Delete")]
        public async Task<IActionResult> Delete()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
				DeleteSchoolClassViewModel viewModel = new DeleteSchoolClassViewModel
				{
					AppUserId = currentUserId,
					AllExistingLetters = await _schoolServices.GetAllExistingLetters()
				};

				return View(viewModel);
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}

        // GET - Delete one class
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteSchoolClassViewModel viewModel)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
				await _schoolServices.DeleteClass(viewModel);

				return RedirectToAction("Delete");
			}
			else
			{
				TempData["Error"] = "You must log in to continue";
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
