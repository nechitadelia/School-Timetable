﻿using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Models
{
    public class Professor
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public int AssignedHours { get; set; }
        public int MaxHours { get; set; } = 20;
        public SchoolSubject ProfessorSubject { get; set; }
        public int SchoolSubjectId { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
