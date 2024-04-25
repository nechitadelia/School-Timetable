using Microsoft.AspNetCore.Mvc;
using School_Timetable.Models;
using System.Diagnostics;

namespace School_Timetable.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
        {
            return View();
        }

    }
}
