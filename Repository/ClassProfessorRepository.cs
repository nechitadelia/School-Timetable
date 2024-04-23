using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace School_Timetable.Repository
{
	public class ClassProfessorRepository : IClassProfessorRepository
	{
		private readonly AppDbContext _dbContext;

		public ClassProfessorRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		//assign one professor to one class
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
        public Professor GetProfessorOfASubjectOfOneClass(SchoolClass schoolClass, SchoolSubject schoolSubject)
		{
			if (ConnectionExists(schoolClass, schoolSubject))
			{
				int professorId = _dbContext.ClassProfessors
				.Where(s => s.SchoolClassId == schoolClass.Id && s.SubjectName == schoolSubject.Name)
				.First().ProfessorId;

				return _dbContext.Professors.Where(p => p.Id == professorId).First();
			}
			else
			{
				return null;
			}
		}

        //get all the class ids for one professor
        public ICollection<int> GetClassIds(Professor professor)
        {
            if (ConnectionExists(professor))
            {
                //saving all the ids of the classes that a professor has
                return _dbContext.ClassProfessors
				.Where(p => p.ProfessorId == professor.Id)
				.Select(s => s.SchoolClassId)
				.ToList();
			}
            else
            {
                return [];
            }
        }

        //get the list of classes for one professor
        public List<SchoolClass> GetClassesOfAProfessor(Professor professor)
		{
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            ICollection<int> schoolClassesIds = GetClassIds(professor);

            //creating the list of School Classes based in the collection of ids
			if (schoolClassesIds != null)
			{
                foreach (int id in schoolClassesIds)
                {
                    SchoolClass s = _dbContext.SchoolClasses.Where(c => c.Id == id).First();
                    schoolClasses.Add(s);
                }

                //ordering the school classes
                schoolClasses = schoolClasses.OrderBy(s => s.YearOfStudy).ThenBy(s => s.ClassLetter).ToList();
            }

            return schoolClasses;
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

		//unassign a professor from all classes
		public void UnassignAProfessorFromAllClasses(Professor professor)
		{
			ICollection<ClassProfessor> cp = _dbContext.ClassProfessors.Where(p => p.ProfessorId == professor.Id).ToList();
			DeleteClassProfessor(cp);
		}

		//unassign all professors from all classes
		public void UnassignAllProfessorsFromAllClasses()
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
