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
			//getting a list of all classes
			SchoolClassCollectionsViewModel classCollections = await _schoolServices.GetClassCollections();

			return View(classCollections);
		}

        // GET - Create a class
        [HttpGet]
        [Route("/SchoolClasses/Create")]
        public async Task<IActionResult> Create()
        {
			string currentUserId = "";
			CreateSchoolClassViewModel viewModel = new CreateSchoolClassViewModel
			{
				AppUserId = currentUserId,
				AllAvailableLetters = await _schoolServices.GetAllAvailableLetters()
			};

			return View(viewModel);
		}

        // POST - Create a class
        [HttpPost]
        public async Task<IActionResult> Create(CreateSchoolClassViewModel viewModel)
        {
			await _schoolServices.AddClass(viewModel);

			return RedirectToAction("Create");
		}

        // GET - Graduate all classes
        [HttpGet]
        [Route("/SchoolClasses/GraduateClasses")]
        public async Task<IActionResult> GraduateClasses()
        {
			//getting a list of all classes
			SchoolClassCollectionsViewModel classCollections = await _schoolServices.GetClassCollections();

			return View(classCollections);
		}

        // POST - Graduate all classes
        [HttpPost]
        public async Task<IActionResult> GraduateClasses(SchoolClassCollectionsViewModel viewModel)
        {
			await _schoolServices.GraduateClasses();
			return RedirectToAction("Index");
		}

        // GET - Delete one class
        [HttpGet]
        [Route("/SchoolClasses/Delete")]
        public async Task<IActionResult> Delete()
        {
			string currentUserId = "";
			DeleteSchoolClassViewModel viewModel = new DeleteSchoolClassViewModel
			{
				AppUserId = currentUserId,
				AllExistingLetters = await _schoolServices.GetAllExistingLetters()
			};

			return View(viewModel);
		}

        // POST - Delete one class
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteSchoolClassViewModel viewModel)
        {
			await _schoolServices.DeleteClass(viewModel);

			return RedirectToAction("Delete");
		}
	}
}
