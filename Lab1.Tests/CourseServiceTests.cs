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

        private CourseService GetService()
        {
            var service =  new CourseService();
            return service;
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
        
        [Fact]
        public void GetCourseById_ReturnsCorrectCourse()
        {
            // Arrange
            var service = GetService();
            var course1 = new OnlineCourse(1, "Курс 1", "P1", "url");
            var course2 = new OfflineCourse(2, "Курс 2", "Desc2", "Loc2", DateTime.Now);
            service.AddCourse(course1);
            service.AddCourse(course2);

            // Act
            var retrievedCourse = service.GetCourseById(2);

            // Assert
            Assert.NotNull(retrievedCourse);
            Assert.Equal(course2.Id, retrievedCourse.Id);
            Assert.Equal("Курс 2", retrievedCourse.Title);
        }
        
        [Fact]
        public void GetCourseById_ReturnsNullForNonExistentCourse()
        {
            // Arrange
            var service = GetService();

            // Act
            var retrievedCourse = service.GetCourseById(999);

            // Assert
            Assert.Null(retrievedCourse);
        }
        
        [Fact]
        public void GetAllCourses_ReturnsAllAddedCourses()
        {
            // Arrange
            var service = GetService();
            var course1 = new OnlineCourse(1, "Курс 1", "Desc1", "url1", "P1");
            var course2 = new OfflineCourse(2, "Курс 2", "Desc2", "Loc2", DateTime.Now);
            service.AddCourse(course1);
            service.AddCourse(course2);

            // Act
            var allCourses = service.GetAllCourses();

            // Assert
            Assert.Equal(2, allCourses.Count());
            Assert.Contains(course1, allCourses);
            Assert.Contains(course2, allCourses);
        }
        
        [Fact]
        public void RemoveCourse_RemovesCourseAndUpdatesRelatedData()
        {
            // Arrange
            var service = GetService();
            var teacher1 = new Teacher(1, "Проф", "Иванов");
            service.AddTeacher(teacher1);
            var course1 = new OnlineCourse(1, "Курс для удаления", "Desc", "url", "P");
            service.AddCourse(course1);
            service.AssignTeacherToCourse(1, 1);

            // Act
            bool result = service.RemoveCourse(1);

            // Assert
            Assert.True(result);
            Assert.Null(service.GetCourseById(1));
            Assert.Equal(0, service.GetAllCourses().Count());
            Assert.True(teacher1.CoursesTaught.Count == 0);
        }
        
        [Fact]
        public void RemoveCourse_ReturnsFalseIfCourseNotFound()
        {
            // Arrange
            var service = GetService();

            // Act
            bool result = service.RemoveCourse(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddTeacher_AddsTeacherToList()
        {
            // Arrange
            var service = GetService();
            var newTeacher = new Teacher(1, "Тестовый Преподаватель");

            // Act
            service.AddTeacher(newTeacher);
            var retrievedTeacher = service.GetAllTeachers().FirstOrDefault(t => t.Id == 1);

            // Assert
            Assert.NotNull(retrievedTeacher);
            Assert.Equal("Тестовый Преподаватель", retrievedTeacher.Name);
            Assert.Equal(1, service.GetAllTeachers().Count());
        }

        [Fact]
        public void AssignTeacherToCourse_AssignsTeacherSuccessfully()
        {
            // Arrange
            var service = GetService();
            var teacher = new Teacher(1, "Проф. А");
            var course = new OnlineCourse(1, "Курс А", "Desc", "url", "P");
            service.AddTeacher(teacher);
            service.AddCourse(course);

            // Act
            bool result = service.AssignTeacherToCourse(1, 1);

            // Assert
            Assert.True(result);
            var updatedCourse = service.GetCourseById(1);
            Assert.NotNull(updatedCourse);
            Assert.Equal(teacher.Id, updatedCourse.AssignedTeacher.Id);
            Assert.Contains(course, teacher.CoursesTaught);
        }

        [Fact]
        public void AssignTeacherToCourse_ReassignsTeacherIfAlreadyAssigned()
        {
            // Arrange
            var service = GetService();
            var teacher1 = new Teacher(1, "Проф. А");
            var teacher2 = new Teacher(2, "Проф. Б");
            var course = new OnlineCourse(1, "Курс А", "Desc", "url", "P");
            service.AddTeacher(teacher1);
            service.AddTeacher(teacher2);
            service.AddCourse(course);

            service.AssignTeacherToCourse(1, 1);

            // Act
            bool result = service.AssignTeacherToCourse(1, 2);

            // Assert
            Assert.True(result);
            var updatedCourse = service.GetCourseById(1);
            Assert.NotNull(updatedCourse);
            Assert.Equal(teacher2.Id, updatedCourse.AssignedTeacher.Id);
            Assert.False(teacher1.CoursesTaught.Any(c => c.Id == course.Id));
            Assert.Contains(course, teacher2.CoursesTaught);
        }

        [Fact]
        public void AssignTeacherToCourse_ReturnsFalseIfCourseNotFound()
        {
            // Arrange
            var service = GetService();
            var teacher = new Teacher(1, "Проф. А");
            service.AddTeacher(teacher);

            // Act
            bool result = service.AssignTeacherToCourse(999, 1);

            // Assert
            Assert.False(result);
            Assert.False(teacher.CoursesTaught.Any());
        }

        [Fact]
        public void AssignTeacherToCourse_ReturnsFalseIfTeacherNotFound()
        {
            // Arrange
            var service = GetService();
            var course = new OnlineCourse(1, "Курс А", "Desc", "url", "P");
            service.AddCourse(course);

            // Act
            bool result = service.AssignTeacherToCourse(1, 999);

            // Assert
            Assert.False(result);
            Assert.Null(course.AssignedTeacher);
        }

        [Fact]
        public void UnassignTeacherFromCourse_UnassignsTeacherSuccessfully()
        {
            // Arrange
            var service = GetService();
            var teacher = new Teacher(1, "Проф. А");
            var course = new OnlineCourse(1, "Курс А", "Desc", "url", "P");
            service.AddTeacher(teacher);
            service.AddCourse(course);
            service.AssignTeacherToCourse(1, 1);

            // Act
            bool result = service.UnassignTeacherFromCourse(1);

            // Assert
            Assert.True(result);
            var updatedCourse = service.GetCourseById(1);
            Assert.NotNull(updatedCourse);
            Assert.Null(updatedCourse.AssignedTeacher);
            Assert.False(teacher.CoursesTaught.Any());
        }

        [Fact]
        public void UnassignTeacherFromCourse_ReturnsFalseIfCourseNotFound()
        {
            // Arrange
            var service = GetService();

            // Act
            bool result = service.UnassignTeacherFromCourse(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddStudentToCourse_AddsStudentSuccessfully()
        {
            // Arrange
            var service = GetService();
            var student = new Student(101, "Иван");
            var course = new OnlineCourse(1, "Курс", "Desc", "url", "P");
            service.AddStudent(student);
            service.AddCourse(course);

            // Act
            bool result = service.AddStudentToCourse(1, 101);

            // Assert
            Assert.True(result);
            var enrolledStudents = service.GetStudentsOnCourse(1);
            Assert.Single(enrolledStudents);
            Assert.Contains(student, enrolledStudents);
        }

        [Fact]
        public void AddStudentToCourse_ReturnsFalseIfStudentAlreadyEnrolled()
        {
            // Arrange
            var service = GetService();
            var student = new Student(101, "Иван");
            var course = new OnlineCourse(1, "Курс", "Desc", "url", "P");
            service.AddStudent(student);
            service.AddCourse(course);
            service.AddStudentToCourse(1, 101);

            // Act
            bool result = service.AddStudentToCourse(1, 101);

            // Assert
            Assert.False(result);
            Assert.Single(service.GetStudentsOnCourse(1));
        }

        [Fact]
        public void RemoveStudentFromCourse_RemovesStudentSuccessfully()
        {
            // Arrange
            var service = GetService();
            var student = new Student(101, "Иван");
            var course = new OnlineCourse(1, "Курс", "Desc", "url", "P");
            service.AddStudent(student);
            service.AddCourse(course);
            service.AddStudentToCourse(1, 101);

            // Act
            bool result = service.RemoveStudentFromCourse(1, 101);

            // Assert
            Assert.True(result);
            Assert.Empty(service.GetStudentsOnCourse(1));
        }

        [Fact]
        public void RemoveStudentFromCourse_ReturnsFalseIfStudentNotFoundOnCourse()
        {
            // Arrange
            var service = GetService();
            var student = new Student(101, "Иван");
            var course = new OnlineCourse(1, "Курс", "Desc", "url", "P");
            service.AddStudent(student);
            service.AddCourse(course);

            // Act
            bool result = service.RemoveStudentFromCourse(1, 101);

            // Assert
            Assert.False(result);
            Assert.Empty(service.GetStudentsOnCourse(1));
        }

        [Fact]
        public void GetCoursesByTeacher_ReturnsCorrectCourses()
        {
            // Arrange
            var service = GetService();
            var teacher1 = new Teacher(1, "Проф. А");
            var teacher2 = new Teacher(2, "Проф. Б");
            var course1 = new OnlineCourse(1, "Курс 1", "Desc", "url", "P");
            var course2 = new OnlineCourse(2, "Курс 2", "Desc", "url", "P");
            var course3 = new OnlineCourse(3, "Курс 3", "Desc", "url", "P");

            service.AddTeacher(teacher1);
            service.AddTeacher(teacher2);
            service.AddCourse(course1);
            service.AddCourse(course2);
            service.AddCourse(course3);

            service.AssignTeacherToCourse(1, 1);
            service.AssignTeacherToCourse(2, 1);
            service.AssignTeacherToCourse(3, 2);

            // Act
            var coursesByTeacher1 = service.GetCoursesByTeacher(1);
            var coursesByTeacher2 = service.GetCoursesByTeacher(2);

            // Assert
            Assert.Equal(2, coursesByTeacher1.Count());
            Assert.Contains(course1, coursesByTeacher1);
            Assert.Contains(course2, coursesByTeacher1);
            Assert.DoesNotContain(course3, coursesByTeacher1);

            Assert.Single(coursesByTeacher2);
            Assert.Contains(course3, coursesByTeacher2);
        }

        [Fact]
        public void GetCoursesByTeacher_ReturnsEmptyListIfTeacherNotFound()
        {
            // Arrange
            var service = GetService();

            // Act
            var courses = service.GetCoursesByTeacher(999);

            // Assert
            Assert.Empty(courses);
        }

        [Fact]
        public void GetCourseDetails_ReturnsCorrectDetailsForOnlineCourse()
        {
            // Arrange
            var course = new OnlineCourse(1, "Онлайн Курс", "Описание", "PlatformX", "http://example.com/online");

            // Act
            string details = course.GetCourseDetails();

            // Assert
            Assert.Contains("Ссылка на занятия - http://example.com/online", details);
            Assert.Contains("Платформа - PlatformX", details);
        }

        [Fact]
        public void GetCourseDetails_ReturnsCorrectDetailsForOfflineCourse()
        {
            // Arrange
            var schedule = new DateTime(2024, 1, 15, 14, 0, 0);
            var course = new OfflineCourse(2, "Офлайн Курс", "Описание", "Room 101", schedule);

            // Act
            string details = course.GetCourseDetails();

            // Assert
            Assert.Contains("Локация - Room 101", details);
            Assert.Contains($"Расписание - {schedule.ToShortDateString()}", details);
        }

        [Fact]
        public void GetInstance_ReturnsTheSameInstanceEveryTime()
        {
            // Arrange
            var service1 = CourseService.GetInstance();

            // Act
            var service2 = CourseService.GetInstance();

            // Assert
            Assert.Same(service1, service2);
        }
    }
}