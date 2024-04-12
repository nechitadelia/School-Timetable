﻿using School_Timetable.Models.Entities;
using School_Timetable.Utilities;
using System.ComponentModel.DataAnnotations;

namespace School_Timetable.Models
{
    public class ProfessorCollectionsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SchoolSubject ProfessorSubject { get; set; }
        public int UnassignedHours { get; set; }
        public List<SchoolClass>? ClassesOfProfessor { get; set; }
    }
}