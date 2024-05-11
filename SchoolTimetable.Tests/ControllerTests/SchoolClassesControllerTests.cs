using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Timetable.Controllers;
using School_Timetable.Interfaces;
using School_Timetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.ControllerTests
{
    public class SchoolClassesControllerTests
    {
        private readonly ISchoolServices _schoolServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SchoolClassesController _schoolClassesController;

        public SchoolClassesControllerTests()
        {
            //Dependencies
            _schoolServices = A.Fake<ISchoolServices>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            //SUT
            _schoolClassesController = new SchoolClassesController(_schoolServices, _httpContextAccessor);
        }

        [Fact]
        public void SchoolClassesController_Index_ReturnsSuccess()
        {
            //Arrange
            SchoolClassCollectionsViewModel classCollections = A.Fake<SchoolClassCollectionsViewModel>();
            A.CallTo(() => _schoolServices.GetClassCollections()).Returns(classCollections);

            //Act
            var result = _schoolClassesController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void SchoolClassesController_GraduateClassesGet_ReturnsSuccess()
        {
            //Arrange
            SchoolClassCollectionsViewModel classCollections = A.Fake<SchoolClassCollectionsViewModel>();
            A.CallTo(() => _schoolServices.GetClassCollections()).Returns(classCollections);

            //Act
            var result = _schoolClassesController.GraduateClasses();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void SchoolClassesController_GraduateClassesPost_ReturnsSuccess()
        {
            //Arrange
            SchoolClassCollectionsViewModel classCollections = A.Fake<SchoolClassCollectionsViewModel>();
            A.CallTo(() => _schoolServices.GraduateClasses()).Returns(true);

            //Act
            var result = _schoolClassesController.GraduateClasses(classCollections);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void SchoolClassesController_Delete_ReturnsSuccess()
        {
            //Arrange
            DeleteSchoolClassViewModel viewModel = A.Fake<DeleteSchoolClassViewModel>();
            A.CallTo(() => _schoolServices.DeleteClass(viewModel)).Returns(true);

            //Act
            var result = _schoolClassesController.Delete(viewModel);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
