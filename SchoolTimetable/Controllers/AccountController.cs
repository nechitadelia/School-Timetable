using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Services;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace School_Timetable.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ISchoolServices schoolServices, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _schoolServices = schoolServices;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET - Login a user
        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
		{
			LoginViewModel response = new LoginViewModel();
            return View(response);
		}

        // POST - Login a user
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
                bool passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
			}
			return View(loginViewModel);
        }

        // GET - Register a new user
		[HttpGet]
		[Route("/Register")]
		public IActionResult Register()
		{
			RegisterViewModel response = new RegisterViewModel();
			return View(response);
		}

        // POST - Register a new user
		[HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(registerViewModel);
			}

			var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = "This email address is already in use";
				return View(registerViewModel);
			}

			var newUser = new AppUser()
			{
				Email = registerViewModel.EmailAddress,
				UserName = registerViewModel.EmailAddress,
				SchoolName = registerViewModel.SchoolName,
				County = registerViewModel.County,
				City = registerViewModel.City
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

			if(newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
				return RedirectToAction("Login");
			}

			return View(registerViewModel);
		}

        //GET - View a user's info
		[HttpGet]
        [Route("/Info")]
        public async Task<IActionResult> Info()
		{
			AppUserViewModel viewModel = await _schoolServices.GetUserViewModel();
			return View(viewModel);
		}

        // GET - Edit a user's info
        [HttpGet]
        [Route("/Info/Edit")]
        public async Task<IActionResult> Edit()
        {
			AppUser currentUser = await _schoolServices.GetUser();

			EditAppUserViewModel viewModel = new EditAppUserViewModel
			{
				Id = currentUser.Id,
				SchoolName = currentUser.SchoolName,
				County = currentUser.County,
				City = currentUser.City
			};

			return View(viewModel);
		}

        // POST - Edit a user's info
        [HttpPost]
        [Route("/Info/Edit")]
        public async Task<IActionResult> Edit(EditAppUserViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				await _schoolServices.EditUser(viewModel);
				return RedirectToAction("Info");
			}

			return View(viewModel);
		}

        // GET - Change user password
        [HttpGet]
        [Route("/Info/Edit/ChangePassword")]
        public IActionResult ChangePassword()
        {
			string currentUserId = "";
			ChangePasswordUserViewModel viewModel = new ChangePasswordUserViewModel { Id = currentUserId };

			return View(viewModel);
		}

        // POST - Change user password
        [HttpPost]
        [Route("/Info/Edit/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserViewModel viewModel)
        {
			if (ModelState.IsValid)
			{
				AppUser currentUser = await _schoolServices.GetUser();

				var result = await _userManager.ChangePasswordAsync(currentUser, viewModel.CurrentPassword, viewModel.NewPassword);

				if (result.Succeeded)
				{
					TempData["Message"] = "Password changed successfully!";
					return RedirectToAction("Info");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}

			TempData["Error"] = "Password error";
			return View(viewModel);
		}

        // GET - View all users
        [HttpGet]
        [Route("/AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
			List<AppUserViewModel> allUsers = await _schoolServices.GetAllUsers();
			return View(allUsers);
		}

        // GET - Delete a user
        [HttpGet]
        [Route("/Account/DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
			AppUser user = await _schoolServices.GetUser(userId);

			AppUserViewModel viewModel = new AppUserViewModel
			{
				Id = user.Id,
				SchoolName = user.SchoolName,
				County = user.County,
				City = user.City,
				EmailAddress = user.Email
			};

			return View(viewModel);
		}

        // POST - Delete a user
        [HttpPost]
        [Route("/Account/DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(AppUserViewModel viewModel)
        {
			if (viewModel != null)
			{
				await _schoolServices.DeleteUser(viewModel);
			}

			return RedirectToAction("AllUsers");
		}

        // POST - Logout a user
        [HttpPost]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}
	}
}
