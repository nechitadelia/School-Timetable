using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.ViewModels;
using System.Collections.Generic;

namespace School_Timetable.Services
{
    public class SchoolServices : ISchoolServices
	{
		private readonly IProfessorRepository _professorRepository;
		private readonly ISchoolClassRepository _schoolClassRepository;
		private readonly ISubjectRepository _subjectRepository;
		private readonly IClassProfessorRepository _classProfessorRepository;
        private readonly IAppUserRepository _appUserRepository;

        public SchoolServices(IProfessorRepository professorRepository, ISchoolClassRepository schoolClassRepository, ISubjectRepository subjectRepository, IClassProfessorRepository classProfessorRepository, IAppUserRepository appUserRepository)
		{
			_professorRepository = professorRepository;
			_schoolClassRepository = schoolClassRepository;
			_subjectRepository = subjectRepository;
			_classProfessorRepository = classProfessorRepository;
            _appUserRepository = appUserRepository;
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

		//get list of all fifth grade classes
		public Stack<SchoolClass> GetFifthGradeClasses()
		{
			return _schoolClassRepository.GetClassesofOneYear(5);
		}

		//get list of all sixth grade classes
		public Stack<SchoolClass> GetSixthGradeClasses()
		{
			return _schoolClassRepository.GetClassesofOneYear(6);
		}

		//get list of all seventh grade classes
		public Stack<SchoolClass> GetSeventhGradeClasses()
		{
			return _schoolClassRepository.GetClassesofOneYear(7);
		}

		//get list of all eighth grade classes
		public Stack<SchoolClass> GetEighthGradeClasses()
		{
			return _schoolClassRepository.GetClassesofOneYear(8);
		}

		//get one subject by id
		public SchoolSubject GetSchoolSubject(int subjectId)
		{
			return _subjectRepository.GetSchoolSubject(subjectId);
		}

		//get one professor by id
		public Professor GetProfessor(int professorId)
		{
			return _professorRepository.GetProfessor(professorId);
		}

        //get a professor's unassigned hours
        public int GetUnassignedHours(int professorId)
        {
			return _professorRepository.GetUnassignedHours(professorId);
        }

		//get a professor's subject by his/her id
		public SchoolSubject GetSubjectOfProfessor(int professorId)
		{
			return _professorRepository.GetSubjectOfProfessor(professorId);
		}

        //get the list of classes for one professor
        public List<SchoolClass> GetClassesOfAProfessor(Professor professor)
        {
			return _classProfessorRepository.GetClassesOfAProfessor(professor);
        }

        //get a list of all professors for each school subject - for Subjects View
        public List<List<Professor>> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects)
		{
			List<List<Professor>> professors = new List<List<Professor>>();

			foreach (SchoolSubject sub in subjects)
			{
				List<Professor> prof = _subjectRepository.GetProfessorsOfASubject(sub.Id);
                prof = prof.OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToList();
                professors.Add(prof);
			}

			return professors;
		}

        //get the list of all subjects for fifth grade
        public List<SchoolSubject> GetSubjectsForFifthGrade()
		{
			return _subjectRepository.GetClassSubjects(5);
		}

		//get the list of all subjects for fifth grade
		public List<SchoolSubject> GetSubjectsForSixthGrade()
		{
			return _subjectRepository.GetClassSubjects(6);
		}

		//get the list of all subjects for fifth grade
		public List<SchoolSubject> GetSubjectsForSeventhGrade()
		{
			return _subjectRepository.GetClassSubjects(7);
		}

		//get the list of all subjects for fifth grade
		public List<SchoolSubject> GetSubjectsForEighthGrade()
		{
			return _subjectRepository.GetClassSubjects(8);
		}

        //get a list of professors for one class
        public List<Professor> GetProfessorsOfAClass(SchoolClass schoolClass)
        {
            List<Professor> professors = new List<Professor>();
			List<SchoolSubject> classSubjects = _subjectRepository.GetClassSubjects(schoolClass.YearOfStudy);

            foreach (SchoolSubject sub in classSubjects)
            {
                Professor prof = _classProfessorRepository.GetProfessorOfASubjectOfOneClass(schoolClass, sub);
                professors.Add(prof);
            }

            return professors;
        }

        //get the list of all professors for one year of study
        public List<List<Professor>> GetProfessorsForOneYearOfStudy(Stack<SchoolClass> schoolClasses)
		{
			List<List<Professor>> classesProfessors = new List<List<Professor>>();

			foreach (SchoolClass schoolClass in schoolClasses)
			{
				classesProfessors.Add(GetProfessorsOfAClass(schoolClass)); //get the list of all professors of one class
			}

			return classesProfessors;
		}

		//get all the available letters for all school years
		public List<char> GetAllAvailableLetters()
		{
			List<char> availableLetters = new List<char>();

			for (int year = 5; year <= 8; year++)
			{
				availableLetters.Add(_schoolClassRepository.GetAvailableLetter(year));
			}

			return availableLetters;
		}

		//get all the existing last letters for all school years
		public List<char> GetAllExistingLetters()
		{
			List<char> existingLetters = new List<char>();

			for (int year = 5; year <= 8; year++)
			{
				existingLetters.Add(_schoolClassRepository.GetLastLetter(year));
			}

			return existingLetters;
		}

        //check if there are any subjects in database
        public bool CheckExistingSubjects()
		{
			return _subjectRepository.CheckExistingSubjects();
		}

        //get all class collections (classes, subjects, professors)
        public SchoolClassCollectionsViewModel GetClassCollections()
		{
			Stack<SchoolClass> fifthGradeClasses = GetFifthGradeClasses();
			Stack<SchoolClass> sixthGradeClasses = GetSixthGradeClasses();
            Stack<SchoolClass> seventhGradeClasses = GetSeventhGradeClasses();
            Stack<SchoolClass> eighthGradeClasses = GetEighthGradeClasses();

			SchoolClassCollectionsViewModel classCollections = new SchoolClassCollectionsViewModel() 
			{
                fifthGradeClasses = fifthGradeClasses,
				sixthGradeClasses = sixthGradeClasses,
				seventhGradeClasses = seventhGradeClasses,
				eighthGradeClasses = eighthGradeClasses,

                fifthGradeSubjects = GetSubjectsForFifthGrade(),
				sixthGradeSubjects = GetSubjectsForSixthGrade(),
				seventhGradeSubjects = GetSubjectsForSeventhGrade(),
				eighthGradeSubjects = GetSubjectsForEighthGrade(),

                fifthGradeProfessors = GetProfessorsForOneYearOfStudy(fifthGradeClasses),
				sixthGradeProfessors = GetProfessorsForOneYearOfStudy(sixthGradeClasses),
				seventhGradeProfessors = GetProfessorsForOneYearOfStudy(seventhGradeClasses),
				eighthGradeProfessors = GetProfessorsForOneYearOfStudy(eighthGradeClasses)
            };

			return classCollections;
        }

		//get a collection of all professors
		public List<ProfessorViewModel> GetProfessorCollections(string currentUserId)
		{
            List<ProfessorViewModel> professorCollections = new List<ProfessorViewModel>();

			ICollection<Professor> allProfessors = GetAllProfessors();

            foreach (Professor professor in allProfessors)
			{
				professorCollections.Add(new ProfessorViewModel
				{
					Id = professor.Id,
					FirstName = professor.FirstName,
					LastName = professor.LastName,
					ProfessorSubject = GetSubjectOfProfessor(professor.Id),
					UnassignedHours = GetUnassignedHours(professor.Id),
					ClassesOfProfessor = GetClassesOfAProfessor(professor),
                    AppUserId = currentUserId
                });
			}

			return professorCollections;
        }

        //get a collection of all subjects
        public List<SchoolSubjectViewModel> GetSubjectsCollections(string currentUserId)
		{
			List<SchoolSubjectViewModel> subjectCollections = new List<SchoolSubjectViewModel>();

			ICollection<SchoolSubject> allSubjects = GetAllSchoolSubjects();

			foreach (SchoolSubject subject in allSubjects)
			{
				//check in which years of study is the subject taught
				List<int> yearsOfStudy = new List<int>();
				if (subject.FifthYearOfStudy == 'Y')
				{
					yearsOfStudy.Add(5);
				}
                if (subject.SixthYearOfStudy == 'Y')
                {
                    yearsOfStudy.Add(6);
                }
                if (subject.SeventhYearOfStudy == 'Y')
                {
                    yearsOfStudy.Add(7);
                }
                if (subject.EighthYearOfStudy == 'Y')
                {
                    yearsOfStudy.Add(8);
                }

                //add the subject to collection
                subjectCollections.Add(new SchoolSubjectViewModel
				{
					Id = subject.Id,
					Name = subject.Name,
					HoursPerWeek = subject.HoursPerWeek,
                    YearsOfStudy = yearsOfStudy,
					Professors = _subjectRepository.GetProfessorsOfASubject(subject.Id),
					AppUserId = currentUserId
				});
			}

			return subjectCollections;
		}

		//get one user by id
		public AppUser GetUser()
		{
			return _appUserRepository.GetUser();
		}

		//get one user view model
		public AppUserViewModel GetUserViewModel()
		{
			return _appUserRepository.GetUserViewModel();
        }

        //-----------------------------------> CREATE METHODS <-----------------------------------

        //adding a new subject to database
        public void AddSubject(CreateSchoolSubjectViewModel viewModel)
		{
			_subjectRepository.AddSubject(viewModel);
		}
        //adding a new professor to database
        public void AddProfessor(CreateProfessorViewModel viewModel)
		{
            _professorRepository.AddProfessor(viewModel);
		}

		//adding a new class to database
		public void AddClass(CreateSchoolClassViewModel viewModel)
		{
			_schoolClassRepository.AddClass(viewModel);
		}

		//assign one professor to one class
		public void AssignOneProfessorToOneClass(SchoolClass schoolClass, Professor professor)
		{
            SchoolSubject professorSubject = professor.ProfessorSubject;

            if (_professorRepository.CanAssignHours(professor.Id) && _professorRepository.CanAssignClass(schoolClass, professorSubject))
            {
                _classProfessorRepository.AddProfessorToAClass(schoolClass, professor); //assigning a professor to one class subject
                _professorRepository.AssignHours(professor.Id);
            }
        }

		//assign all professors to all classes
		public void AssignAllProfessorsToAllClasses()
		{
			ICollection<SchoolClass> schoolClasses = GetAllClasses();

			foreach (SchoolClass schoolClass in schoolClasses) //iterating through all the classes of a school
			{
				ICollection<SchoolSubject> classSubjects = _subjectRepository.GetClassSubjects(schoolClass.YearOfStudy);

				foreach (SchoolSubject subject in classSubjects) //iterating through all the subjects of one class
				{
					ICollection<Professor> professors = _subjectRepository.GetProfessorsOfASubject(subject.Id);
					professors = professors.OrderBy(p => p.Id)
						.ToList();

					if (professors.Count > 0)
					{
						foreach (Professor p in professors) //iterating through all the professors of one subject
						{
                            AssignOneProfessorToOneClass(schoolClass, p);
                        }
					}
				}
			}
		}

		//-----------------------------------> UPDATE METHODS <-----------------------------------

		//edit a professors's data
		public void EditProfessor(EditProfessorViewModel viewModel)
		{
			_professorRepository.EditProfessor(viewModel);
		}

		//edit a user data
		public void EditUser(EditAppUserViewModel viewModel)
		{
			_appUserRepository.EditUser(viewModel);
		}

        //graduate all classes - change classes to the next school year
        public void GraduateClasses()
		{
            //unassign eighth grade classes from connections
            Stack<SchoolClass> eighthGradeClasses = GetEighthGradeClasses();

			foreach (SchoolClass schoolClass in eighthGradeClasses)
			{
                //get the list of professors for the class that will be deleted
                List<Professor> professors = GetProfessorsOfAClass(schoolClass);

                //unassign hours from all professors who were teaching that class
                foreach (Professor p in professors)
                {
                    if (p != null)
                    {
                        _professorRepository.UnassignHoursFromProfessor(p);
                    }
                }

                //unassign class from connections
                _classProfessorRepository.UnassignAClass(schoolClass);

                //delete the class from database
                _schoolClassRepository.DeleteClass(schoolClass);
            }

			//graduate the rest of the classes (5-7)
            _schoolClassRepository.GraduateClasses();
        }

        //-----------------------------------> DELETE METHODS <-----------------------------------

        //delete a subject from database
        public bool DeleteSchoolSubject(SchoolSubject subject)
		{
			//get existing professors of the subject
			ICollection<Professor> professors = _subjectRepository.GetProfessorsOfASubject(subject.Id);

			//check if there are existing professors connected to the subject
			if (professors.Count != 0)
			{
				return false;
			}
			else
			{
				//delete subject from database only if it has no professors
				_subjectRepository.DeleteSchoolSubject(subject);
				return true;
			}
		}

		//delete a professor from database
		public void DeleteProfessor(Professor professor)
		{
			//get the classes of the deleted professor
			List<SchoolClass> classesOfProfessor = GetClassesOfAProfessor(professor);
			SchoolSubject professorSubject = GetSubjectOfProfessor(professor.Id);

            //unassign the professor from all classes
            _classProfessorRepository.UnassignAProfessorFromAllClasses(professor);

			//delete professor from database
			_professorRepository.DeleteProfessor(professor);

			//reassign the classes to the rest of the professors, if there are any left/available
			ICollection<Professor> professors = _subjectRepository.GetProfessorsOfASubject(professorSubject.Id);
			professors = professors.OrderBy(p => p.Id).ToList();

            if (professors.Count > 0)
			{
                foreach (SchoolClass schoolClass in classesOfProfessor)
                {
                    if (professors.Count > 0)
                    {
                        foreach (Professor p in professors)
                        {
                            AssignOneProfessorToOneClass(schoolClass, p);
                        }
                    }
                }
            }
        }

		//delete a class from database
		public void DeleteClass(DeleteSchoolClassViewModel viewModel)
		{
			//find out which class will be deleted, based on user input
			SchoolClass schoolClass = _schoolClassRepository.GetLastClassFromOneYear(viewModel.YearOfStudy);

            if (schoolClass != null)
			{
                //get the list of professors for the class that will be deleted
                List<Professor> professors = GetProfessorsOfAClass(schoolClass);

                //unassign hours from all professors who were teaching that class
                foreach (Professor p in professors)
                {
                    if (p != null)
					{
                        _professorRepository.UnassignHoursFromProfessor(p);
                    }
                }

                //unassign class from connections
                _classProfessorRepository.UnassignAClass(schoolClass);

                //delete the class from database
                _schoolClassRepository.DeleteClass(schoolClass);
            }
		}

        //unassign all professors from all classes
        public void UnAssignAllProfessorsFromClasses()
		{
			_classProfessorRepository.UnassignAllProfessorsFromAllClasses();
			_professorRepository.UnassignAllHoursFromEveryone();
		}
	}
}
