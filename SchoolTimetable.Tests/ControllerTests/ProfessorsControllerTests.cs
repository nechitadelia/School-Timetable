using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Timetable.Controllers;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.ControllerTests
{
    public class ProfessorsControllerTests
    {
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProfessorsController _professorsController;

        public ProfessorsControllerTests()
        {
            //Dependencies
            _schoolServices = A.Fake<ISchoolServices>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            //SUT
            _professorsController = new ProfessorsController(_schoolServices, _httpContextAccessor);
        }


        [Fact]
        public void ProfessorsController_Index_ReturnsSuccess()
        {
            //Arrange
            string currentUserId = "1";
            List<ProfessorViewModel> professorsCollections = A.Fake<List<ProfessorViewModel>>();
            A.CallTo(() => _schoolServices.GetProfessorCollections(currentUserId)).Returns(professorsCollections);
            A.CallTo(() => _schoolServices.CheckExistingSubjects()).Returns(true);

            //Act
            var result = _professorsController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProfessorsController_Create_ReturnsSuccess()
        {
            //Arrange
            ICollection<SchoolSubject> subjects = A.Fake<ICollection<SchoolSubject>>();
            A.CallTo(() => _schoolServices.GetAllSchoolSubjects()).Returns(subjects);

            CreateProfessorViewModel viewModel = A.Fake<CreateProfessorViewModel>();
            A.CallTo(() => _schoolServices.AddProfessor(viewModel)).Returns(true);

            //Act
            var result = _professorsController.Create(viewModel);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProfessorsController_Assign_ReturnsSuccess()
        {
            //Arrange
            string currentUserId = "1";
            List<ProfessorViewModel> professorsCollections = A.Fake<List<ProfessorViewModel>>();
            A.CallTo(() => _schoolServices.GetProfessorCollections(currentUserId)).Returns(professorsCollections);
            A.CallTo(() => _schoolServices.AssignAllProfessorsToAllClasses()).Returns(true);

            //Act
            var result = _professorsController.Assign();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProfessorsController_UnAssignAll_ReturnsSuccess()
        {
            //Arrange
            string currentUserId = "1";
            List<ProfessorViewModel> professorsCollections = A.Fake<List<ProfessorViewModel>>();
            A.CallTo(() => _schoolServices.GetProfessorCollections(currentUserId)).Returns(professorsCollections);
            A.CallTo(() => _schoolServices.UnAssignAllProfessorsFromClasses()).Returns(true);

            //Act
            var result = _professorsController.UnAssignAll();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProfessorsController_Edit_ReturnsSuccess()
        {
            //Arrange
            Professor professor = A.Fake<Professor>();
            int professorId = professor.Id;
            A.CallTo(() => _schoolServices.GetProfessor(professorId)).Returns(professor);

            //Act
            var result = _professorsController.Edit(professorId);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ProfessorsController_Delete_ReturnsSuccess()
        {
            //Arrange
            Professor professor = A.Fake<Professor>();
            int professorId = professor.Id;
            A.CallTo(() => _schoolServices.GetProfessor(professorId)).Returns(professor);

            //Act
            var result = _professorsController.Delete(professorId);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}