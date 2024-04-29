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
        private readonly ILogger<HomeController> _logger;
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("/Home")]
        public IActionResult Index()
        {
            AppUserViewModel viewModel = _schoolServices.GetUserViewModel();

            if (viewModel != null)
            {
                return View(viewModel);
            }
            else
            {
                return View();
            }
            
        }

    }
}
