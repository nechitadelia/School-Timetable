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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.ControllerTests
{
    public class SchoolSubjectsControllerTests
    {
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SchoolSubjectsController _schoolSubjectsController;

        public SchoolSubjectsControllerTests()
        {
            //Dependencies
            _schoolServices = A.Fake<ISchoolServices>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            //SUT
            _schoolSubjectsController = new SchoolSubjectsController(_schoolServices, _httpContextAccessor);
        }


        [Fact]
        public void SchoolSubjectsController_Index_ReturnsSuccess()
        {
            //Arrange
            string currentUserId = "1";
            List<SchoolSubjectViewModel> subjects = A.Fake<List<SchoolSubjectViewModel>>();
            A.CallTo(() => _schoolServices.GetSubjectsCollections(currentUserId)).Returns(subjects);

            //Act
            var result = _schoolSubjectsController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void SchoolSubjectsController_Delete_ReturnsSuccess()
        {
            //Arrange
            SchoolSubject subject = A.Fake<SchoolSubject>();
            int subjectId = 1;
            A.CallTo(() => _schoolServices.GetSchoolSubject(subjectId)).Returns(subject);

            //Act
            var result = _schoolSubjectsController.Delete(subjectId);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }


}
