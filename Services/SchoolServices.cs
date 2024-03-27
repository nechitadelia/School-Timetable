using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using System.Collections.Generic;

namespace School_Timetable.Services
{
	public class SchoolServices : ISchoolServices
	{
		private readonly IProfessorRepository _professorRepository;
		private readonly ISchoolClassRepository _schoolClassRepository;
		private readonly ISubjectRepository _subjectRepository;
		private readonly IClassProfessorRepository _classProfessorRepository;

		public SchoolServices(IProfessorRepository professorRepository, ISchoolClassRepository schoolClassRepository, ISubjectRepository subjectRepository, IClassProfessorRepository classProfessorRepository)
		{
			_professorRepository = professorRepository;
			_schoolClassRepository = schoolClassRepository;
			_subjectRepository = subjectRepository;
			_classProfessorRepository = classProfessorRepository;
		}

		//-----------------------------------> READ METHODS <-----------------------------------

		//getting a list of all classes from the repository
		public ICollection<SchoolClass> GetAllClasses()
		{
			return _schoolClassRepository.GetAllClasses();
		}
		
		//getting a list of all professors from the repository
		public ICollection<Professor> GetAllProfessors()
		{
			return _professorRepository.GetProfessors();
		}

		//getting a list of all subjects from the repository
		public ICollection<SchoolSubject> GetAllSchoolSubjects()
		{
			return _subjectRepository.GetSchoolSubjects();
		}

		//get one professor by id
		public Professor GetProfessor(int professorId)
		{
			return _professorRepository.GetProfessor(professorId);
		}

		//getting a list of subjects for all professors
		public List<string> GetAllProfessorsSubjects(ICollection<Professor> professors)
		{
			List<string> professorSubjects = new List<string>();
			foreach (Professor p in professors)
			{
				SchoolSubject sub = _professorRepository.GetProfessorSubject(p.Id);
				professorSubjects.Add(sub.Name);
			}
			return professorSubjects;
		}

		//getting a list of unassigned hours for all professors
		public List<int> GetAllProfessorsUnassignedHours(ICollection<Professor> professors)
		{
			List<int> unassignedHours = new List<int>();
			foreach (Professor p in professors)
			{
				unassignedHours.Add(_professorRepository.GetUnassignedHours(p.Id));
			}
			return unassignedHours;
		}

		//getting a list of the classes for all professors
		public List<List<string>> GetAllProfessorsClasses(ICollection<Professor> professors)
		{
			List<List<string>> professorClasses = new List<List<string>>();
			foreach (Professor p in professors)
			{
				professorClasses.Add(_classProfessorRepository.GetClassesOfAProfessor(p));
			}
			return professorClasses;
		}

		//get a professor's subject by his/her id
		public SchoolSubject GetProfessorSubject(int professorId)
		{
			return _professorRepository.GetProfessorSubject(professorId);
		}

		//getting a list of strings with all professors, in the order of school subjects
		public List<string> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects)
		{
			List<string> professors = new List<string>();

			foreach (SchoolSubject sub in subjects)
			{
				string prof = _subjectRepository.ProfessorsListToString(sub.Id);
				prof = prof.TrimEnd(',');
				professors.Add(prof);
			}

			return professors;
		}

		//getting the list of all subjects for all classes
		public List<List<SchoolSubject>> GetSubjectsForAllClasses(ICollection<SchoolClass> schoolClasses)
		{
			List<List<SchoolSubject>> classesSubjects = new List<List<SchoolSubject>>();

			foreach (SchoolClass schoolClass in schoolClasses)
			{
				List<SchoolSubject> subjects = _schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy); //get the list of all subjects for one class
				classesSubjects.Add(subjects);
			}

			return classesSubjects;
		}

		//getting the list of all professors for all classes
		public List<List<string>> GetProfessorsForAllClasses(ICollection<SchoolClass> schoolClasses)
		{
			List<List<string>> classesProfessors = new List<List<string>>();

			foreach (SchoolClass schoolClass in schoolClasses)
			{
				List<SchoolSubject> subjects = _schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy); //get the list of all subjects for one class
				classesProfessors.Add(_classProfessorRepository.GetProfessorsOfAClass(schoolClass, subjects)); //get the list of all professors of one class
			}

			return classesProfessors;
		}

		//get the next available letter for a new class, depending on the user input for the year of study
		public char GetAvailableLetter(SchoolClassViewModel viewModel)
		{
			return _schoolClassRepository.GetAvailableLetter(viewModel.YearOfStudy);
		}

		//-----------------------------------> CREATE METHODS <-----------------------------------

		//adding a new professor to database
		public void AddProfessor(ProfessorViewModel viewModel, ICollection<SchoolSubject> schoolSubjects)
		{
			_professorRepository.AddProfessor(viewModel, schoolSubjects);
		}

		//adding a new class to database
		public void AddClass(SchoolClassViewModel viewModel)
		{
			_schoolClassRepository.AddClass(viewModel.YearOfStudy);
		}

		//assign all professors to all classes
		public void AssignAllProfessorsToAllClasses()
		{
			ICollection<SchoolClass> schoolClasses = _schoolClassRepository.GetAllClasses();

			foreach (SchoolClass schoolClass in schoolClasses) //iterating through all the classes of a school
			{
				ICollection<SchoolSubject> classSubjects = _schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy);

				foreach (SchoolSubject subject in classSubjects) //iterating through all the subjects of one class
				{
					ICollection<Professor> professors = _subjectRepository.GetProfessorsOfASubject(subject.Id);

					if (professors != null)
					{
						foreach (Professor p in professors) //iterating through all the professors of one subject
						{
							if (_professorRepository.CanAssignHours(p.Id) && _professorRepository.CanAssignClass(schoolClass, subject))
							{
								_classProfessorRepository.AddProfessorToAClass(schoolClass, p); //assigning a professor to one class subject
								_professorRepository.AssignHours(p.Id);
								break;
							}
						}
					}
				}
			}
		}

		//-----------------------------------> UPDATE METHODS <-----------------------------------

		//edit a professors's data
		public void EditProfessor(Professor professor)
		{
			_professorRepository.EditProfessor(professor);
		}

		//-----------------------------------> DELETE METHODS <-----------------------------------

		//delete a professor from database
		public void DeleteProfessor(Professor professor)
		{
			_classProfessorRepository.UnassignAProfessor(professor);
			_professorRepository.DeleteProfessor(professor);
		}

		//delete a class from database
		public void DeleteClass(SchoolClassViewModel viewModel)
		{
			//find out which class will be deleted, based on user input
			SchoolClass schoolClass = _schoolClassRepository.GetClassFromViewModel(viewModel);

			//get the list of professors for the class that will be deleted
			List<Professor> professors = _classProfessorRepository.GetProfessorsOfAClass(schoolClass);

			//unassign hours from all professors who were teaching that class
			foreach (Professor p in professors)
			{
				_professorRepository.UnassignHoursFromProfessor(p);

			}

			//unassign class from connections
			_classProfessorRepository.UnassignAClass(schoolClass);

			//delete the class from database
			_schoolClassRepository.DeleteClass(viewModel.YearOfStudy);
		}

		//unassign all professors from all classes
		public void UnAssignAllProfessorsFromClasses()
		{
			_classProfessorRepository.UnassignAllProfessors();
			_professorRepository.UnassignAllHoursFromEveryone();
		}

	}
}
