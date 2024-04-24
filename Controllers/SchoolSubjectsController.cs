using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
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
        public IActionResult Index()
        {
            string currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<SchoolSubjectViewModel> subjects = _schoolServices.GetSubjectsCollections(currentUserId);

			return View(subjects);
        }


    }
}
