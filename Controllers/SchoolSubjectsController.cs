﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace School_Timetable.Controllers
{
    public class SchoolSubjectsController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public SchoolSubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all school's subjects
            ICollection<SchoolSubject> subjects = _subjectRepository.GetSchoolSubjects();

            //creating a list of strings with all professors, in the order of subjects
            List<string> professors = new List<string>();

            foreach (SchoolSubject sub in subjects)
            {
                string prof = _subjectRepository.ProfessorsListToString(sub.Id);
                prof = prof.TrimEnd(',');
                professors.Add(prof);
            }

            //saving the data of professors, so it can be sent to the View
            ViewData["professors"] = professors;

            return View(subjects);
        }


    }
}
