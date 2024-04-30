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
        AppUser GetUser();

        //get one user by id
        AppUser GetUser(string id);

        //get one app user view model
        AppUserViewModel GetUserViewModel();

        //edit a user data
        void EditUser(EditAppUserViewModel viewModel);

        //delete a user from database
        void DeleteUser(AppUserViewModel viewModel);

        //save changes to database
        void Save();
    }
}
