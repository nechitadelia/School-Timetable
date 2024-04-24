using School_Timetable.Models;

namespace School_Timetable.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
