using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School_Timetable.Controllers;
using School_Timetable.Data;
using School_Timetable.Interfaces;
using School_Timetable.Models;
using School_Timetable.Repository;
using School_Timetable.Services;
using School_Timetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Tests.RepositoryTests
{
    public class SubjectRepositoryTests
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubjectRepository _subjectRepository;
        private readonly AppDbContext _dbContext;

        public SubjectRepositoryTests()
        {
            //Dependencies
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            _dbContext = GetDbContext();

            //SUT
            _subjectRepository = new SubjectRepository(_dbContext, _httpContextAccessor);
        }

        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(options);
            dbContext.Database.EnsureCreated();
            if (dbContext.SchoolSubjects.Count() < 0 )
            {
                for(int i = 0; i < 10; i++)
                {
                    dbContext.SchoolSubjects.Add(new SchoolSubject()
                    {
                        Name = "matematica",
                        HoursPerWeek = 5,
                        FifthYearOfStudy = 'Y',
                        SixthYearOfStudy = 'Y',
                        SeventhYearOfStudy = 'Y',
                        EighthYearOfStudy = 'Y'
                    });
                    dbContext.SaveChanges();
                }
            }
            return dbContext;
        }

        [Fact]
        public async void SubjectRepository_AddSubject_ReturnsBool()
        {
            //Arrange
            CreateSchoolSubjectViewModel schoolSubject = new CreateSchoolSubjectViewModel()
            {
                Name = "matematica",
                HoursPerWeek = 5,
                FifthYearOfStudy = true,
                SixthYearOfStudy = true,
                SeventhYearOfStudy = true,
                EighthYearOfStudy = true
            };

            //Act
            var result = await _subjectRepository.AddSubject(schoolSubject);

            //Assert
            result.Should().BeTrue();
        }
    }
}