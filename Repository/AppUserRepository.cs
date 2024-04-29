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

        //get one user by id
        public AppUser GetUser()
        {
			string? currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			return _dbContext.Users.Where(u => u.Id == currentUserId).First();
        }

        //get one app user view model
        public AppUserViewModel GetUserViewModel()
        {
            AppUser user = GetUser();

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

        //save changes to database
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
