using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School_Timetable.Data;
using School_Timetable.Dtos.Account;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.ViewModels;

namespace School_Timetable.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [HttpGet]
		[Route("/Login")]
		public IActionResult Login()
		{
            //this is meant to save data that was typed in login,in case the user refreshes the page
			LoginViewModel response = new LoginViewModel();
            return View(response);
		}

        [HttpPost]
		[Route("/Login")]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //user is found, check password
                bool passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    //password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //user is found, password incorrect
				return View(loginViewModel);
			}
			//user not found
			return View(loginViewModel);
        }

		[HttpGet]
		[Route("/Register")]
		public IActionResult Register()
		{
			//this is meant to save data that was typed in login,in case the user refreshes the page
			RegisterViewModel response = new RegisterViewModel();
			return View(response);
		}

		[HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(registerViewModel);
			}

			//check if user already exists in database
			var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = "This email address is already in use";
				return View(registerViewModel);
			}

			//if the user doesn't exist, create a new user and add it to database
			var newUser = new AppUser()
			{
				Email = registerViewModel.EmailAddress,
				UserName = registerViewModel.EmailAddress
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

			if(newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
				return RedirectToAction("Index", "Home");
			}

			return View(registerViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
