using Microsoft.AspNetCore.Mvc;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using School_Timetable.Services;

namespace School_Timetable.Controllers
{
	public class ClassProfessorController : Controller
	{
		private readonly ISchoolServices _schoolServices;

		public ClassProfessorController(ISchoolServices schoolServices)
		{
			_schoolServices = schoolServices;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Assign() 
		{
			_schoolServices.AssignAllProfessorsToAllClasses();

			return RedirectToAction("Index");
		}

		public IActionResult UnAssignAll()
		{
			_schoolServices.UnAssignAllProfessorsFromClasses();

			return RedirectToAction("Index");
		}
	}
}
