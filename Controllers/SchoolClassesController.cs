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

        [HttpGet]
        [Route("/SchoolClasses")]
        public IActionResult Index()
        {
            //getting a list of all classes
            SchoolClassCollectionsViewModel classCollections = _schoolServices.GetClassCollections();

			return View(classCollections);
        }

        [HttpGet]
        [Route("/SchoolClasses/Create")]
        public IActionResult Create()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            CreateSchoolClassViewModel viewModel = new CreateSchoolClassViewModel 
            { 
                AppUserId = currentUserId, 
                AllAvailableLetters = _schoolServices.GetAllAvailableLetters()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateSchoolClassViewModel viewModel)
        {
            _schoolServices.AddClass(viewModel);

            return RedirectToAction("Create");
        }

        [HttpGet]
        [Route("/SchoolClasses/GraduateClasses")]
        public IActionResult GraduateClasses()
        {
            //getting a list of all classes
            SchoolClassCollectionsViewModel classCollections = _schoolServices.GetClassCollections();

            return View(classCollections);
        }

        [HttpPost]
        public IActionResult GraduateClasses(SchoolClassCollectionsViewModel viewModel)
        {
            _schoolServices.GraduateClasses();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/SchoolClasses/Delete")]
        public IActionResult Delete()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            DeleteSchoolClassViewModel viewModel = new DeleteSchoolClassViewModel
            {
                AppUserId = currentUserId,
                AllExistingLetters = _schoolServices.GetAllExistingLetters()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(DeleteSchoolClassViewModel viewModel)
        {
            _schoolServices.DeleteClass(viewModel);

			return RedirectToAction("Delete");
		}
	}
}
