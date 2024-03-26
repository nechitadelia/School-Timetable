using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Models.Entities;
using School_Timetable.Repository;

namespace School_Timetable.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IClassProfessorRepository _classProfessorRepository;

        public ProfessorsController(IProfessorRepository professorRepository, ISubjectRepository subjectRepository, IClassProfessorRepository classProfessorRepository)
        {
            _professorRepository = professorRepository;
            _subjectRepository = subjectRepository;
            _classProfessorRepository = classProfessorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //getting a list of all professors from the database
            ICollection<Professor> professors = _professorRepository.GetProfessors();
            
            //creating the view data content
            List<string> professorSubjects = new List<string>();
            List<int> unassignedHours = new List<int>();
            List<List<string>> professorClasses = new List<List<string>>();

            foreach (Professor p in professors)
            {
                //getting the list of subjects for all professors
                SchoolSubject sub = _professorRepository.GetProfessorSubject(p.Id);
                professorSubjects.Add(sub.Name);

                //getting a list of unassigned hours for all professors
                unassignedHours.Add(_professorRepository.GetUnassignedHours(p.Id));

                //getting a list of the classes for all professors
                professorClasses.Add(_classProfessorRepository.GetClassesOfAProfessor(p));
            }

            //saving the data of professors, so it can be sent to the View
            ViewData["professorSubjects"] = professorSubjects;
            ViewData["unassignedHours"] = unassignedHours;
            ViewData["professorClasses"] = professorClasses;

            return View(professors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //getting a list of all subjects from the database
            ICollection<SchoolSubject> schoolSubjects = _subjectRepository.GetSchoolSubjects();

            ViewData["schoolSubjects"] = schoolSubjects;

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProfessorViewModel viewModel)
        {
            //getting a list of all subjects from the database
            ICollection<SchoolSubject> schoolSubjects = _subjectRepository.GetSchoolSubjects();

            //creating and saving the new professor
            _professorRepository.AddProfessor(viewModel, schoolSubjects);

            ViewData["schoolSubjects"] = schoolSubjects;

            return View();
        }

        [HttpGet, ActionName("Edit")]
        public IActionResult Edit(int professorId)
        {
            Professor professor = _professorRepository.GetProfessor(professorId);

            SchoolSubject professorSubject = _professorRepository.GetProfessorSubject(professorId);
			ViewData["professorSubject"] = professorSubject;

            return View(professor);
        }

        [HttpPost]
        public IActionResult Edit(Professor viewModel)
        {
            _professorRepository.EditProfessor(viewModel);

            return RedirectToAction("Index", "Professors");
        }

        [HttpPost]
        public IActionResult Delete(Professor viewModel)
        {
            _classProfessorRepository.UnassignAProfessor(viewModel);
            _professorRepository.DeleteProfessor(viewModel);

            return RedirectToAction("Index", "Professors");
        }
    }
}
