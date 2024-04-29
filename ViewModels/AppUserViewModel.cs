using System.ComponentModel.DataAnnotations;

namespace School_Timetable.ViewModels
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string SchoolName { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
    }
}
