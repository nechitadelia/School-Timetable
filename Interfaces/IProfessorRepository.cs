﻿using Microsoft.EntityFrameworkCore;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Interfaces
{
    public interface IProfessorRepository
    {
        //get list of all professors
        public ICollection<Professor> GetProfessors();

        //get one professor by id
        public Professor GetProfessor(int professorId);

        //get one professor by name
        public Professor GetProfessor(string lastName, string firstName);

        //get a professor's subject by his/her id
        public SchoolSubject GetProfessorSubject(int professorId);

        //get a professor's subject name by his/her name
        //public int GetProfessorSubject(string lastName, string firstName);

        //check if a professor exists
        public bool PofessorExists(int professorId);

        //assign hours to a professor
        public void AssignHours(int professorId);

        //get a professor's unassigned hours
        public int GetUnassignedHours(int professorId);

        //creating a new professor
        public void AddProfessor(ProfessorViewModel viewModel, ICollection<SchoolSubject> schoolSubjects);

        //edit a professors's data
        public void EditProfessor(Professor viewModel);

		//delete a professor from the database
		public void DeleteProfessor(Professor viewModel);

        //save changes to database
        public void Save();
    }
}
