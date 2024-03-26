using Microsoft.AspNetCore.Mvc;
using School_Timetable.Interfaces;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;

namespace School_Timetable.Controllers
{
	public class ClassProfessorController : Controller
	{
		private readonly IClassProfessorRepository _classProfessorRepository;
		private readonly ISchoolClassRepository _schoolClassRepository;
		private readonly ISubjectRepository _subjectRepository;
		private readonly IProfessorRepository _professorRepository;

		public ClassProfessorController(IClassProfessorRepository classProfessorRepository, ISchoolClassRepository schoolClassRepository, ISubjectRepository subjectRepository, IProfessorRepository professorRepository)
		{
			_classProfessorRepository = classProfessorRepository;
			_schoolClassRepository = schoolClassRepository;
			_subjectRepository = subjectRepository;
			_professorRepository = professorRepository;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Assign() 
		{
			ICollection<SchoolClass> schoolClasses = _schoolClassRepository.GetAllClasses();


			foreach(SchoolClass schoolClass in schoolClasses) //iterating through all the classes of a school
			{
				ICollection<SchoolSubject> classSubjects = _schoolClassRepository.GetClassSubjects(schoolClass.YearOfStudy);

				foreach(SchoolSubject subject in classSubjects) //iterating through all the subjects of one class
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
			return RedirectToAction("Index");
		}

		public IActionResult UnAssignAll()
		{
			_classProfessorRepository.UnassignAllProfessors();
			_professorRepository.UnassignAllHours();

			return RedirectToAction("Index");
		}
	}
}
