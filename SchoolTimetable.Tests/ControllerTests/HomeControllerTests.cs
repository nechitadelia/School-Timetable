using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School_Timetable.Controllers;
using School_Timetable.Interfaces;
using School_Timetable.Services;
using School_Timetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly ISchoolServices _schoolServices;
        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            //Dependencies
            _schoolServices = A.Fake<ISchoolServices>();

            //SUT
            _homeController = new HomeController(_schoolServices);
        }

        [Fact]
        public void HomeController_Index_ReturnsSuccess()
        {
            //Arrange
            AppUserViewModel viewModel = A.Fake<AppUserViewModel>();
            A.CallTo(() => _schoolServices.GetUserViewModel()).Returns(viewModel);
            //Act
            var result = _homeController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
