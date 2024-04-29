using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.ViewModels;

namespace School_Timetable.Interfaces
{
    public interface IAppUserRepository
    {
        //get one user by id
        AppUser GetUser();

		//get one app user view model
		AppUserViewModel GetUserViewModel();

        //edit a user data
        void EditUser(EditAppUserViewModel viewModel);

        //save changes to database
        void Save();
    }
}
