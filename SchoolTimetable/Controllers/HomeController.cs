using Microsoft.AspNetCore.Mvc;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
using System.Diagnostics;

namespace School_Timetable.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISchoolServices _schoolServices;

        public HomeController(ISchoolServices schoolServices)
        {
            _schoolServices = schoolServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUserViewModel viewModel = await _schoolServices.GetUserViewModel();
                return View(viewModel);
            }
            else
            {
                return View();
            }
		}

    }
}
