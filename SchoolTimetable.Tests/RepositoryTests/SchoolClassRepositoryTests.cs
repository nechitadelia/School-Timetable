using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.RepositoryTests
{
    public class SchoolClassRepositoryTests
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public SchoolClassRepositoryTests()
        {
            //Dependencies
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            AppDbContext dbContext = GetDbContext();

            //SUT
            _schoolClassRepository = new SchoolClassRepository(dbContext, _httpContextAccessor);
        }

        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated();
            if (dbContext.SchoolClasses.Count() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.SchoolClasses.Add(new SchoolClass()
                    {
                        YearOfStudy = 5,
                        ClassLetter = 'A'
                    });
                    dbContext.SaveChanges();
                }
            }
            return dbContext;
        }

        [Fact]
        public async void SchoolClassRepository_GetAllClasses_ReturnsICollectionSchoolClass()
        {
            //Arrange

            //Act
            var result = await _schoolClassRepository.GetAllClasses();

            //Assert
            result.Should().AllBeOfType<SchoolClass>();
        }

        [Fact]
        public async void SchoolClassRepository_AddClass_ReturnsBool()
        {
            //Arrange
            CreateSchoolClassViewModel schoolClass = new CreateSchoolClassViewModel()
            {
                YearOfStudy = 5,
                ClassLetter = 'B'
            };

            //Act
            var result = await _schoolClassRepository.AddClass(schoolClass);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void SchoolClassRepository_DeleteClass_ReturnsBool()
        {
            //Arrange
            SchoolClass schoolClass = new SchoolClass()
            {
                YearOfStudy = 5,
                ClassLetter = 'C'
            };

            var result = await _schoolClassRepository.DeleteClass(schoolClass);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void SchoolClassRepository_GraduateClasses_ReturnsBool()
        {
            //Arrange

            //Act
            var result = await _schoolClassRepository.GraduateClasses();

            //Assert
            result.Should().BeTrue();
        }
    }
}