using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Utilities;
using School_Timetable.ViewModels;

namespace School_Timetable.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserRepository(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //get all users
        public async Task<List<AppUserViewModel>> GetAllUsers()
        {
            List<AppUserViewModel> appUsersViewModel = new List<AppUserViewModel>();  
            ICollection<AppUser> appUsers = await _dbContext.Users.OrderBy(u => u.County).ToListAsync();

            foreach (AppUser user in appUsers)
            {
                if (user.Email != "admin@gmail.com")
                {
					AppUserViewModel viewModel = new AppUserViewModel
					{
						Id = user.Id,
						SchoolName = user.SchoolName,
						County = user.County,
						City = user.City,
						EmailAddress = user.Email
					};

					appUsersViewModel.Add(viewModel);
				}
            }

            return appUsersViewModel;
        }

        //get one user
        public AppUser GetUser()
        {
			string? currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			return _dbContext.Users.Where(u => u.Id == currentUserId).First();
        }

        //get one user by id
        public AppUser GetUser(string id)
        {
            return _dbContext.Users.Where(u => u.Id == id).First();
        }

        //get one app user view model
        public AppUserViewModel GetUserViewModel()
        {
            AppUser user = GetUser();

            if (user != null)
            {
                AppUserViewModel viewModel = new AppUserViewModel
                {
                    Id = user.Id,
                    SchoolName = user.SchoolName,
                    County = user.County,
                    City = user.City,
                    EmailAddress = user.Email
                };

                return viewModel;
            }

            return new AppUserViewModel();
        }

        //edit a user data
        public void EditUser(EditAppUserViewModel viewModel)
        {
            AppUser user = GetUser();

			if (user != null)
            {
                user.SchoolName = viewModel.SchoolName;
                user.County = viewModel.County;
                user.City = viewModel.City;
                Save();
            }
        }

        //delete a user from database
        public void DeleteUser(AppUserViewModel viewModel)
        {
            AppUser user = GetUser(viewModel.Id);

            _dbContext.Users.Remove(user);
            Save();
        }
        
        //save changes to database
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
