using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace School_Timetable.Controllers
{
    public class SchoolSubjectsController : Controller
    {
		private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SchoolSubjectsController(ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
			_schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/SchoolSubjects")]
        public async Task<IActionResult> Index()
        {
            //getting a list of all school's subjects
            ICollection<SchoolSubject> subjects = await _schoolServices.GetAllSchoolSubjects();

            //saving the data of professors, so it can be sent to the View
            ViewData["professors"] = _schoolServices.GetProfessorsForSubjects(subjects);

			return View(subjects);
        }


    }
}
