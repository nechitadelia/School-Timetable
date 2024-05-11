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
		public async Task<ICollection<SchoolClass>> GetAllClasses()
		{
			return await _schoolClassRepository.GetAllClasses();
		}

		//getting a list of all professors from the repository
		public async Task<ICollection<Professor>> GetAllProfessors()
		{
			return await _professorRepository.GetProfessors();
		}

		//getting a list of all subjects from the repository
		public async Task<ICollection<SchoolSubject>> GetAllSchoolSubjects()
		{
			return await _subjectRepository.GetSchoolSubjects();
		}

		//get list of all fifth grade classes
		public async Task<Stack<SchoolClass>> GetFifthGradeClasses()
		{
			return await _schoolClassRepository.GetClassesofOneYear(5);
		}

		//get list of all sixth grade classes
		public async Task<Stack<SchoolClass>> GetSixthGradeClasses()
		{
			return await _schoolClassRepository.GetClassesofOneYear(6);
		}

		//get list of all seventh grade classes
		public async Task<Stack<SchoolClass>> GetSeventhGradeClasses()
		{
			return await _schoolClassRepository.GetClassesofOneYear(7);
		}

		//get list of all eighth grade classes
		public async Task<Stack<SchoolClass>> GetEighthGradeClasses()
		{
			return await _schoolClassRepository.GetClassesofOneYear(8);
		}

		//get one subject by id
		public async Task<SchoolSubject> GetSchoolSubject(int subjectId)
		{
			return await _subjectRepository.GetSchoolSubject(subjectId);
		}

		//get one professor by id
		public async Task<Professor> GetProfessor(int professorId)
		{
			return await _professorRepository.GetProfessor(professorId);
		}

        //get a professor's unassigned hours
        public async Task<int> GetUnassignedHours(int professorId)
        {
			return await _professorRepository.GetUnassignedHours(professorId);
        }

		//get a professor's subject by his/her id
		public async Task<SchoolSubject> GetSubjectOfProfessor(int professorId)
		{
			return await _professorRepository.GetSubjectOfProfessor(professorId);
		}

        //get the list of classes for one professor
        public async Task<List<SchoolClass>> GetClassesOfAProfessor(Professor professor)
        {
			return await _classProfessorRepository.GetClassesOfAProfessor(professor);
        }

        //get a list of all professors for each school subject - for Subjects View
        public async Task<List<List<Professor>>> GetProfessorsForSubjects(ICollection<SchoolSubject> subjects)
		{
			List<List<Professor>> professors = new List<List<Professor>>();

			foreach (SchoolSubject sub in subjects)
			{
				List<Professor> prof = await _subjectRepository.GetProfessorsOfASubject(sub.Id);
                prof = prof.OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToList();
                professors.Add(prof);
			}

			return professors;
		}

        //get the list of all subjects for fifth grade
        public async Task<List<SchoolSubject>> GetSubjectsForFifthGrade()
		{
			return await _subjectRepository.GetClassSubjects(5);
		}

		//get the list of all subjects for fifth grade
		public async Task<List<SchoolSubject>> GetSubjectsForSixthGrade()
		{
			return await _subjectRepository.GetClassSubjects(6);
		}

		//get the list of all subjects for fifth grade
		public async Task<List<SchoolSubject>> GetSubjectsForSeventhGrade()
		{
			return await _subjectRepository.GetClassSubjects(7);
		}

		//get the list of all subjects for fifth grade
		public async Task<List<SchoolSubject>> GetSubjectsForEighthGrade()
		{
			return await _subjectRepository.GetClassSubjects(8);
		}

        //get a list of professors for one class
        public async Task<List<Professor>> GetProfessorsOfAClass(SchoolClass schoolClass)
        {
            List<Professor> professors = new List<Professor>();
			List<SchoolSubject> classSubjects = await _subjectRepository.GetClassSubjects(schoolClass.YearOfStudy);

            foreach (SchoolSubject sub in classSubjects)
            {
                Professor prof = await _classProfessorRepository.GetProfessorOfASubjectOfOneClass(schoolClass, sub);
                professors.Add(prof);
            }

            return professors;
        }

        //get the list of all professors for one year of study
        public async Task<List<List<Professor>>> GetProfessorsForOneYearOfStudy(Stack<SchoolClass> schoolClasses)
		{
			List<List<Professor>> classesProfessors = new List<List<Professor>>();

			foreach (SchoolClass schoolClass in schoolClasses)
			{
				classesProfessors.Add(await GetProfessorsOfAClass(schoolClass)); //get the list of all professors of one class
			}

			return classesProfessors;
		}

		//get all the available letters for all school years
		public async Task<List<char>> GetAllAvailableLetters()
		{
			List<char> availableLetters = new List<char>();

			for (int year = 5; year <= 8; year++)
			{
				availableLetters.Add(await _schoolClassRepository.GetAvailableLetter(year));
			}

			return availableLetters;
		}

		//get all the existing last letters for all school years
		public async Task<List<char>> GetAllExistingLetters()
		{
			List<char> existingLetters = new List<char>();

			for (int year = 5; year <= 8; year++)
			{
				existingLetters.Add(await _schoolClassRepository.GetLastLetter(year));
			}

			return existingLetters;
		}

        //check if there are any subjects in database
        public async Task<bool> CheckExistingSubjects()
		{
			return await _subjectRepository.CheckExistingSubjects();
		}

        //get all class collections (classes, subjects, professors)
        public async Task<SchoolClassCollectionsViewModel> GetClassCollections()
		{
			Stack<SchoolClass> fifthGradeClasses = await GetFifthGradeClasses();
			Stack<SchoolClass> sixthGradeClasses = await GetSixthGradeClasses();
            Stack<SchoolClass> seventhGradeClasses = await GetSeventhGradeClasses();
            Stack<SchoolClass> eighthGradeClasses = await GetEighthGradeClasses();

			SchoolClassCollectionsViewModel classCollections = new SchoolClassCollectionsViewModel() 
			{
                fifthGradeClasses = fifthGradeClasses,
				sixthGradeClasses = sixthGradeClasses,
				seventhGradeClasses = seventhGradeClasses,
				eighthGradeClasses = eighthGradeClasses,

                fifthGradeSubjects = await GetSubjectsForFifthGrade(),
				sixthGradeSubjects = await GetSubjectsForSixthGrade(),
				seventhGradeSubjects = await GetSubjectsForSeventhGrade(),
				eighthGradeSubjects = await GetSubjectsForEighthGrade(),

                fifthGradeProfessors = await GetProfessorsForOneYearOfStudy(fifthGradeClasses),
				sixthGradeProfessors = await GetProfessorsForOneYearOfStudy(sixthGradeClasses),
				seventhGradeProfessors = await GetProfessorsForOneYearOfStudy(seventhGradeClasses),
				eighthGradeProfessors = await GetProfessorsForOneYearOfStudy(eighthGradeClasses)
            };

			return classCollections;
        }

		//get a collection of all professors
		public async Task<List<ProfessorViewModel>> GetProfessorCollections(string currentUserId)
		{
            List<ProfessorViewModel> professorCollections = new List<ProfessorViewModel>();

			ICollection<Professor> allProfessors = await GetAllProfessors();

            foreach (Professor professor in allProfessors)
			{
				professorCollections.Add(new ProfessorViewModel
				{
					Id = professor.Id,
					FirstName = professor.FirstName,
					LastName = professor.LastName,
					ProfessorSubject = await GetSubjectOfProfessor(professor.Id),
					UnassignedHours = await GetUnassignedHours(professor.Id),
					ClassesOfProfessor = await GetClassesOfAProfessor(professor),
                    AppUserId = currentUserId
                });
			}

			return professorCollections;
        }

        //get a collection of all subjects
        public async Task<List<SchoolSubjectViewModel>> GetSubjectsCollections(string currentUserId)
		{
			List<SchoolSubjectViewModel> subjectCollections = new List<SchoolSubjectViewModel>();

			ICollection<SchoolSubject> allSubjects = await GetAllSchoolSubjects();

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
					Professors = await _subjectRepository.GetProfessorsOfASubject(subject.Id),
					AppUserId = currentUserId
				});
			}

			return subjectCollections;
		}

        //get all users
        public async Task<List<AppUserViewModel>> GetAllUsers()
		{
			return await _appUserRepository.GetAllUsers();
		}

        //get one user
        public async Task<AppUser> GetUser()
		{
			return await _appUserRepository.GetUser();
		}

        //get one user by id
        public async Task<AppUser> GetUser(string id)
        {
            return await _appUserRepository.GetUser(id);
        }

        //get one user view model
        public async Task<AppUserViewModel> GetUserViewModel()
		{
			return await _appUserRepository.GetUserViewModel();
        }

        //-----------------------------------> CREATE METHODS <-----------------------------------

        //adding a new subject to database
        public async Task<bool> AddSubject(CreateSchoolSubjectViewModel viewModel)
		{
			return await _subjectRepository.AddSubject(viewModel);
		}
        //adding a new professor to database
        public async Task<bool> AddProfessor(CreateProfessorViewModel viewModel)
		{
            return await _professorRepository.AddProfessor(viewModel);
		}

		//adding a new class to database
		public async Task<bool> AddClass(CreateSchoolClassViewModel viewModel)
		{
			return await _schoolClassRepository.AddClass(viewModel);
		}

		//assign one professor to one class
		public async Task<bool> AssignOneProfessorToOneClass(SchoolClass schoolClass, Professor professor)
		{
            SchoolSubject professorSubject = professor.ProfessorSubject;

            if (await _professorRepository.CanAssignHours(professor.Id) && await _professorRepository.CanAssignClass(schoolClass, professorSubject))
            {
                bool result1 = await _classProfessorRepository.AddProfessorToAClass(schoolClass, professor); //assigning a professor to one class subject
                bool result2 = await _professorRepository.AssignHours(professor.Id);

                if (result1 == false || result2 == false)
				{
					return false;
				}
            }
			return true;
        }

		//assign all professors to all classes
		public async Task<bool> AssignAllProfessorsToAllClasses()
		{
			ICollection<SchoolClass> schoolClasses = await GetAllClasses();

			foreach (SchoolClass schoolClass in schoolClasses) //iterating through all the classes of a school
			{
				ICollection<SchoolSubject> classSubjects = await _subjectRepository.GetClassSubjects(schoolClass.YearOfStudy);

				foreach (SchoolSubject subject in classSubjects) //iterating through all the subjects of one class
				{
					ICollection<Professor> professors = await _subjectRepository.GetProfessorsOfASubject(subject.Id);
					professors = professors.OrderBy(p => p.Id)
						.ToList();

					if (professors.Count > 0)
					{
						foreach (Professor p in professors) //iterating through all the professors of one subject
						{
							bool result = await AssignOneProfessorToOneClass(schoolClass, p);
							if (result == false)
							{
								return false;
							}
                        }
					}
				}
			}
			return true;
		}

		//-----------------------------------> UPDATE METHODS <-----------------------------------

		//edit a professors's data
		public async Task<bool> EditProfessor(EditProfessorViewModel viewModel)
		{
			return await _professorRepository.EditProfessor(viewModel);
		}

		//edit a user data
		public async Task<bool> EditUser(EditAppUserViewModel viewModel)
		{
			return await _appUserRepository.EditUser(viewModel);
		}

        //graduate all classes - change classes to the next school year
        public async Task<bool> GraduateClasses()
		{
            //unassign eighth grade classes from connections
            Stack<SchoolClass> eighthGradeClasses = await GetEighthGradeClasses();

			foreach (SchoolClass schoolClass in eighthGradeClasses)
			{
                //get the list of professors for the class that will be deleted
                List<Professor> professors = await GetProfessorsOfAClass(schoolClass);

                //unassign hours from all professors who were teaching that class
                foreach (Professor p in professors)
                {
                    if (p != null)
                    {
                        _professorRepository.UnassignHoursFromProfessor(p);
                    }
                }

                //unassign class from connections
                await _classProfessorRepository.UnassignAClass(schoolClass);

                //delete the class from database
                await _schoolClassRepository.DeleteClass(schoolClass);
            }

			//graduate the rest of the classes (5-7)
            return await _schoolClassRepository.GraduateClasses();
        }

        //-----------------------------------> DELETE METHODS <-----------------------------------

        //delete a user from database
        public async Task<bool> DeleteUser(AppUserViewModel viewModel)
		{
			return await _appUserRepository.DeleteUser(viewModel);
		}

        //delete a subject from database
        public async Task<bool> DeleteSchoolSubject(SchoolSubject subject)
		{
			//get existing professors of the subject
			ICollection<Professor> professors = await _subjectRepository.GetProfessorsOfASubject(subject.Id);

			//check if there are existing professors connected to the subject
			if (professors.Count != 0)
			{
				return false;
			}
			else
			{
				//delete subject from database only if it has no professors
				await _subjectRepository.DeleteSchoolSubject(subject);
				return true;
			}
		}

		//delete a professor from database
		public async Task<bool> DeleteProfessor(Professor professor)
		{
			//get the classes of the deleted professor
			List<SchoolClass> classesOfProfessor = await GetClassesOfAProfessor(professor);
			SchoolSubject professorSubject = await GetSubjectOfProfessor(professor.Id);

            //unassign the professor from all classes
            await _classProfessorRepository.UnassignAProfessorFromAllClasses(professor);

			//delete professor from database
			bool result = await _professorRepository.DeleteProfessor(professor);

			//reassign the classes to the rest of the professors, if there are any left/available
			ICollection<Professor> professors = await _subjectRepository.GetProfessorsOfASubject(professorSubject.Id);
			professors = professors.OrderBy(p => p.Id).ToList();

            if (professors.Count > 0)
			{
                foreach (SchoolClass schoolClass in classesOfProfessor)
                {
                    if (professors.Count > 0)
                    {
                        foreach (Professor p in professors)
                        {
                            await AssignOneProfessorToOneClass(schoolClass, p);
                        }
                    }
                }
            }

            if (result) { return true; }
			else { return false; }
        }

		//delete a class from database
		public async Task<bool> DeleteClass(DeleteSchoolClassViewModel viewModel)
		{
			//find out which class will be deleted, based on user input
			SchoolClass schoolClass = await _schoolClassRepository.GetLastClassFromOneYear(viewModel.YearOfStudy);

            if (schoolClass != null)
			{
                //get the list of professors for the class that will be deleted
                List<Professor> professors = await GetProfessorsOfAClass(schoolClass);

                //unassign hours from all professors who were teaching that class
                foreach (Professor p in professors)
                {
                    if (p != null)
					{
                        _professorRepository.UnassignHoursFromProfessor(p);
                    }
                }

                //unassign class from connections
                await _classProfessorRepository.UnassignAClass(schoolClass);

                //delete the class from database
                bool result = await _schoolClassRepository.DeleteClass(schoolClass);

                if (result == false) { return false; }
            }

			return true;
		}

        //unassign all professors from all classes
        public async Task<bool> UnAssignAllProfessorsFromClasses()
		{
			bool result1 = await _classProfessorRepository.UnassignAllProfessorsFromAllClasses();
			bool result2 = await _professorRepository.UnassignAllHoursFromEveryone();

			if (result1 || result2)
			{
				return true;
			}
			return false;
		}
	}
}
