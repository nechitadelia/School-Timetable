using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
