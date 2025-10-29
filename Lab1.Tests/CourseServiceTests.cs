using Xunit;
using System;
using System.Linq;
using Lab1.Interfaces;
using Lab1.Models;
using Lab1.Services;

namespace Lab1.Tests
{
    public class CourseServiceTests
    {

        private ICourseService GetService()
        {
            return CourseService.GetInstance();
        }
        
        [Fact]
        public void AddCourse_AddsCourseToList()
        {
            // Arrange (Подготовка)
            var service = GetService();
            var newCourse = new OnlineCourse(1, "Тестовый курс", "Описание", "zoom", "url123");

            // Act (Выполнение)
            service.AddCourse(newCourse);
            var retrievedCourse = service.GetCourseById(1);

            // Assert (Проверка)
            Assert.NotNull(retrievedCourse);
            Assert.Equal(newCourse.Id, retrievedCourse.Id);
            Assert.Equal(newCourse.Title, retrievedCourse.Title);
            Assert.Equal(1, service.GetAllCourses().Count());
        }
    }
}