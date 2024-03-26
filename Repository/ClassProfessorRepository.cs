using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Models;
using System.Collections.Generic;

namespace School_Timetable.Repository
{
	public class ClassProfessorRepository : IClassProfessorRepository
	{
		private readonly AppDbContext _dbContext;

		public ClassProfessorRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		//creating a new professor
		public async void AddProfessorToAClass(SchoolClass schoolClass, Professor professor)
		{
			//searching the name of the subject based on the id
			string subjectName = _dbContext.SchoolSubjects
				.Where(s => s.Id == professor.SchoolSubjectId)
				.First().Name;

			//creating a new connection between a class and a professor
			ClassProfessor classProfessor = new ClassProfessor
			{
				SchoolClassId = schoolClass.Id,
				SubjectName = subjectName,
				ProfessorId = professor.Id
			};

			await _dbContext.ClassProfessors.AddAsync(classProfessor);
			Save();
		}

		//check if a connection exists in the ClassProfessor table
		public bool ConnectionExists(SchoolClass schoolClass, SchoolSubject schoolSubject)
		{
			return _dbContext.ClassProfessors
				.Any(s => s.SchoolClassId == schoolClass.Id && s.SubjectName == schoolSubject.Name);
		}

        public bool ConnectionExists(Professor professor)
        {
            return _dbContext.ClassProfessors
                .Any(p => p.ProfessorId == professor.Id);
        }

        //get the professor of one subject of a class
        public string GetProfessorOfASubject(SchoolClass schoolClass, SchoolSubject schoolSubject)
		{
            if (ConnectionExists(schoolClass, schoolSubject) == true)
			{
                int professorId = _dbContext.ClassProfessors
                .Where(s => s.SchoolClassId == schoolClass.Id && s.SubjectName == schoolSubject.Name)
                .First().ProfessorId;

                Professor p = _dbContext.Professors.Where(p => p.Id == professorId).First();

                return ($"{p.LastName} {p.FirstName}");
            }
			else
			{
				return "";
            }
		}

		//get a list of professors for one class
		public List<string> GetProfessorsOfAClass(SchoolClass schoolClass, List<SchoolSubject> classSubjects)
		{
			List<string> professors = new List<string>();

			foreach (SchoolSubject sub in classSubjects)
			{
                string prof = GetProfessorOfASubject(schoolClass, sub);
                professors.Add(prof);
			}

			return professors;
		}

        //get all the class ids for one professor
        public ICollection<int> GetClassIds(Professor professor)
        {
            if (ConnectionExists(professor) == true)
            {
                //saving all the ids of the classes that a professor has
                ICollection<int> schoolClassesIds = _dbContext.ClassProfessors
                .Where(p => p.ProfessorId == professor.Id)
                .Select(s => s.SchoolClassId)
				.ToList();

                return schoolClassesIds;
            }
            else
            {
                return [];
            }
        }

        //get the list of classes for one professor
        public List<string> GetClassesOfAProfessor(Professor professor)
		{
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            List<string> schoolClassesString = new List<string>();
            ICollection<int> schoolClassesIds = GetClassIds(professor);

            //creating the list of School Classes based in the collection of ids
            foreach (var id in schoolClassesIds)
            {
                SchoolClass s = _dbContext.SchoolClasses.Where(c => c.Id == id).First();
                schoolClasses.Add(s);
            }

			//ordering the school classes
			schoolClasses = schoolClasses.OrderBy(s => s.YearOfStudy).ThenBy(s => s.ClassLetter).ToList();

            //creating the list of class names in a list of string
            foreach (var s in schoolClasses)
			{
				schoolClassesString.Add($"{s.YearOfStudy}{s.ClassLetter}");
            }

            return schoolClassesString;
        }

		//delete a collection of ClassProfessor from database
		public void DeleteClassProfessor(ICollection<ClassProfessor> classProfessors)
		{
            foreach (ClassProfessor cp in classProfessors)
            {
                _dbContext.ClassProfessors.Remove(cp);
                Save();
            }
        }

		//unassign a professor from classes
		public void UnassignAProfessor(Professor professor)
		{
			ICollection<ClassProfessor> cp = _dbContext.ClassProfessors.Where(p => p.ProfessorId == professor.Id).ToList();
			DeleteClassProfessor(cp);
		}

		//unassign all professors from all classes
		public void UnassignAllProfessors()
		{
			ICollection<ClassProfessor> cp = _dbContext.ClassProfessors.ToList();
			DeleteClassProfessor(cp);
        }

		//delete a class from ClassProfessor table
		public void UnassignAClass(SchoolClass schoolClass)
		{
			ICollection<ClassProfessor> cp = _dbContext.ClassProfessors.Where(c => c.SchoolClassId == schoolClass.Id).ToList();
			DeleteClassProfessor(cp);
		}

		//save changes to database
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
