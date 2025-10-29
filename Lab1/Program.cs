using System;
using Lab1.Models;
using Lab1.Services;

namespace Lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Lab1: Hello World!");
            
            CourseService courseService = new CourseService();
            StartDemo(courseService);
        }

        private static void StartDemo(CourseService courseService)
        {
            // Добавляем преподавателей и студентов
            var teacher1 = new Teacher(1, "Профессор","Иванов");
            var teacher2 = new Teacher(2, "Доцент"," Петрова");
            courseService.AddTeacher(teacher1);
            courseService.AddTeacher(teacher2);

            var student1 = new Student(101, "Иван","Сидоров");
            var student2 = new Student(102, "Мария","Кузнецова");
            var student3 = new Student(103, "Петр","Смирнов");
            courseService.AddStudent(student1);
            courseService.AddStudent(student2);
            courseService.AddStudent(student3);
            
            // Добавляем курсы
            var onlineCourse1 = new OnlineCourse(1, "Введение в C#", "Zoom", "zoom.com/c#");
            var offlineCourse1 = new OfflineCourse(2, "Алгебра", "Аудитория 301", new DateTime(2025, 9, 1, 10, 0, 0));
            var onlineCourse2 = new OnlineCourse(3, "Базы данных", "Zoom", "zoom.com/bd");

            courseService.AddCourse(onlineCourse1);
            courseService.AddCourse(offlineCourse1);
            courseService.AddCourse(onlineCourse2);
            
            // Назначаем преподавателей на курсы
            courseService.AssignTeacherToCourse(1, teacher1.Id); // C# -> Профессор Иванов
            courseService.AssignTeacherToCourse(2, teacher2.Id); // Алгебра -> Доцент Петрова
            courseService.AssignTeacherToCourse(3, teacher1.Id); // Базы данных -> Профессор Иванов
            
            // Записываем студентов на курсы
            courseService.AddStudentToCourse(1, student1.Id); // C# -> Иван Сидоров
            courseService.AddStudentToCourse(1, student2.Id); // C# -> Мария Кузнецова
            courseService.AddStudentToCourse(2, student1.Id); // Алгебра -> Иван Сидоров
            courseService.AddStudentToCourse(2, student3.Id); // Алгебра -> Петр Смирнов
            courseService.AddStudentToCourse(3, student1.Id); // Базы данных -> Иван Сидоров
            courseService.AddStudentToCourse(3, student2.Id); // Базы данных -> Мария Кузнецова
            courseService.AddStudentToCourse(3, student3.Id); // Базы данных -> Петр Смирнов
            
            // Проверка
            Console.WriteLine("\n--- Все курсы ---");
            foreach (var course in courseService.GetAllCourses())
            {
                Console.WriteLine(course.GetCourseDetails());
            }

            Console.WriteLine("\n--- Студенты на курсе 'Введение в C#' (ID: 1) ---");
            foreach (var student in courseService.GetStudentsOnCourse(1))
            {
                Console.WriteLine(student.ToString());
            }

            Console.WriteLine("\n--- Курсы, которые ведет Профессор Иванов (ID: 1) ---");
            foreach (var course in courseService.GetCoursesByTeacher(1))
            {
                Console.WriteLine(course.ToString());
            }
            
            courseService.RemoveStudentFromCourse(onlineCourse1.Id, student1.Id);
            Console.WriteLine("\n--- Студенты на курсе 'Введение в C#' (ID: 1) ---");
            foreach (var student in courseService.GetStudentsOnCourse(1))
            {
                Console.WriteLine(student.ToString());
            }
        }
    }
}