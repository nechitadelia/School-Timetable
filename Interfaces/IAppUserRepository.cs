using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.ViewModels;

namespace School_Timetable.Interfaces
{
    public interface IAppUserRepository
    {
        //get all users
        Task<List<AppUserViewModel>> GetAllUsers();

        //get one user
        Task<AppUser> GetUser();

        //get one user by id
        Task<AppUser> GetUser(string id);

        //get one app user view model
        Task<AppUserViewModel> GetUserViewModel();

        //edit a user data
        Task EditUser(EditAppUserViewModel viewModel);

        //delete a user from database
        Task DeleteUser(AppUserViewModel viewModel);

        //save changes to database
        bool Save();
    }
}
