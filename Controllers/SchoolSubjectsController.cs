using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using School_Timetable.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace School_Timetable.Controllers
{
    public class SchoolSubjectsController : Controller
    {
		private readonly ISchoolServices _schoolServices;

		public SchoolSubjectsController(ISchoolServices schoolServices)
        {
			_schoolServices = schoolServices;
		}

        [HttpGet]
        [Route("/SchoolSubjects")]
        public IActionResult Index()
        {
            //getting a list of all school's subjects
            ICollection<SchoolSubject> subjects = _schoolServices.GetAllSchoolSubjects();

            //saving the data of professors, so it can be sent to the View
            ViewData["professors"] = _schoolServices.GetProfessorsForSubjects(subjects);

			return View(subjects);
        }


    }
}
