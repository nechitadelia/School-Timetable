using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;

namespace School_Timetable.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _dbContext;

        public ProfessorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get list of all professors
        public ICollection<Professor> GetProfessors()
        {
            return _dbContext.Professors
                .OrderBy(p => p.Id)
                .ToList();
        }

        //get one professor by id
        public Professor GetProfessor(int professorId)
        {
            return _dbContext.Professors
                .Find(professorId);
        }

        //get one professor by name
        public Professor GetProfessor(string lastName, string firstName)
        {
            return _dbContext.Professors
                .Where(p => p.LastName == lastName && p.FirstName == firstName)
                .FirstOrDefault();
        }

        //get a professor's subject by his/her id
        public SchoolSubject GetProfessorSubject(int professorId)
        {
            Professor professor = GetProfessor(professorId);
            int subjectId = professor.SchoolSubjectId;

            SchoolSubject subject = _dbContext.SchoolSubjects
                .Where(s => s.Id == subjectId)
                .First();

            return subject;
        }

        //get a professor's subject name by his/her name
        //public int GetProfessorSubject(string lastName, string firstName)
        //{
        //    Professor professor = GetProfessor(lastName, firstName);
        //    return professor.SchoolSubjectId;
        //}

        //check if a professor exists
        public bool PofessorExists(int professorId)
        {
            return _dbContext.Professors.Any(p => p.Id == professorId);
        }

		//check if you can assign hours to a professor
		public bool CanAssignHours(int professorId)
		{
			Professor professor = GetProfessor(professorId);
			SchoolSubject subject = GetProfessorSubject(professorId);

			if ((professor.AssignedHours + subject.HoursPerWeek) <= 20)
			{
                return true;
			}
            else
            {
                return false;
            }
		}

        //check if a professor was already assigned to a class
        public bool CanAssignClass(SchoolClass schoolClass, SchoolSubject schoolSubject)
        {
            //check if there is already a professor for the same class and subject
            bool result = _dbContext.ClassProfessors.Any(c => c.SchoolClassId == schoolClass.Id && c.SubjectName == schoolSubject.Name);
            
            if (result)
            {
                return false;
            }
            return true;
        }

		//assign hours to a professor
		public void AssignHours(int professorId)
        {
            Professor professor = GetProfessor(professorId);
			SchoolSubject subject = GetProfessorSubject(professorId);

			if (CanAssignHours(professorId))
            {
				professor.AssignedHours += subject.HoursPerWeek;
			}
        }

        //get a professor's unassigned hours
        public int GetUnassignedHours(int professorId)
        {
            Professor professor = GetProfessor(professorId);
            int unassignedHours = 20 - professor.AssignedHours;

            return unassignedHours;
        }

		//unassign all hours from a professor
		public void UnassignAllHoursFromProfessor(Professor professor)
		{
            professor.AssignedHours = 0;
            Save();
		}

		//unassign all hours from all professors
		public void UnassignAllHoursFromEveryone()
        {
            ICollection<Professor> professors = GetProfessors();

            foreach (Professor p in professors)
            {
				UnassignAllHoursFromProfessor(p);
            }
		}

        //unassign hours from a professor (when a class is deleted)
        public void UnassignHoursFromProfessor(Professor professor)
        {
            int subjectHours = GetProfessorSubject(professor.Id).HoursPerWeek;
			professor.AssignedHours -= subjectHours;
		}

		//creating a new professor
		public async void AddProfessor(ProfessorViewModel viewModel, ICollection<SchoolSubject> schoolSubjects)
        {
            //cheching which subject was chosen by the user in the view
            SchoolSubject subject = new SchoolSubject();

            foreach (SchoolSubject sub in schoolSubjects)
            {
                if (sub.Name == viewModel.SchoolSubjectName)
                {
                    subject = sub;
                    break;
                }
            }

            //creating the new professor entity from the user input
            Professor professor = new Professor
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                AssignedHours = 0,
                //SchoolSubject = subject,
                SchoolSubjectId = subject.Id
            };

            await _dbContext.Professors.AddAsync(professor);
            Save();

		}

		//edit a professors's data
		public void EditProfessor(Professor viewModel)
		{
            Professor professor = GetProfessor(viewModel.Id);

            if (professor != null)
			{
				professor.FirstName = viewModel.FirstName;
				professor.LastName = viewModel.LastName;

				Save();
			}
		}

		//delete a professor from the database
		public void DeleteProfessor(Professor viewModel)
        {
            Professor professor = _dbContext.Professors
                .AsNoTracking()
                .First(p => p.Id == viewModel.Id);

            _dbContext.Professors.Remove(professor);
			Save();
		}

		//save changes to database
		public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
