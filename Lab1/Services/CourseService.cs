using Lab1.Models.Abstract;

namespace Lab1.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Interfaces;
using Lab1.Models;
using Lab1.Models.Abstract;

public class CourseService : ICourseService
{
    private readonly List<Course> _courses = new List<Course>();
    private readonly List<Teacher> _teachers = new List<Teacher>();
    private readonly List<Student> _students = new List<Student>();
    
    private int _nextCourseId = 1;
    private int _nextTeacherId = 1;
    private int _nextStudentId = 1;
    
    private Teacher? FindTeacherById(int teacherId)
    {
        return _teachers.FirstOrDefault(t => t.Id == teacherId);
    }

    private Student? FindStudentById(int studentId)
    {
        return _students.FirstOrDefault(s => s.Id == studentId);
    }
    
    public void AddCourse(Course course)
    {
        if (_courses.Any(c => c.Id == course.Id))
        {
            throw new Exception("Course already exists");
        }
        _courses.Add(course);
    }

    public bool RemoveCourse(int courseId)
    {
        // Находим курс для удаления
        var courseToRemove = _courses.FirstOrDefault(c => c.Id == courseId);
        
        if (courseToRemove != null)
        {
            // Очищаем связи курса с преподами и студентами
            courseToRemove.ClearCourse();
            return _courses.Remove(courseToRemove);
        }
        return false; // Курс не найден
    }

    public Course? GetCourseById(int courseId)
    {
        return _courses.FirstOrDefault(c => c.Id == courseId);
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _courses.AsReadOnly();
    }

    public IEnumerable<Course> GetCoursesByTeacher(int teacherId)
    {
        var teacher = FindTeacherById(teacherId);
        if (teacher == null) return Enumerable.Empty<Course>();

        return teacher.CoursesTaught.AsReadOnly();
    }

    public bool AssignTeacherToCourse(int courseId, int teacherId)
    {
        var course = GetCourseById(courseId);
        if (course == null) return false;

        var teacher = FindTeacherById(teacherId);
        if (teacher == null) return false;

        // Если на курсе уже есть преподаватель, снимаем его сначала
        if (course.AssignedTeacher != null)
        {
            course.AssignedTeacher.CoursesTaught.Remove(course);
        }

        course.AssignedTeacher = teacher;
        teacher.CoursesTaught.Add(course);
        return true;
    }

    public bool UnassignTeacherFromCourse(int courseId)
    {
        var course = GetCourseById(courseId);
        if (course == null) return false;

        if (course.AssignedTeacher != null)
        {
            course.AssignedTeacher.CoursesTaught.Remove(course);
            course.AssignedTeacher = null;
            return true;
        }
        return false;
    }

    public bool AddStudentToCourse(int courseId, int studentId)
    {
        var course = GetCourseById(courseId);
        if (course == null) return false;

        var student = FindStudentById(studentId);
        if (student == null) return false;

        if (course.EnrolledStudents.All(s => s.Id != studentId))
        {
            course.EnrolledStudents.Add(student);
            return true;
        }
        return false;
    }

    public bool RemoveStudentFromCourse(int courseId, int studentId)
    {
        var course = GetCourseById(courseId);
        if (course == null) return false;

        var student = FindStudentById(studentId);
        if (student == null) return false;

        return course.EnrolledStudents.Remove(student);
    }

    public IEnumerable<Student> GetStudentsOnCourse(int courseId)
    {
        var course = GetCourseById(courseId);
        if (course == null) return Enumerable.Empty<Student>();

        return course.EnrolledStudents.AsReadOnly();
    }
    
    // Добавление преподавателей и студентов в систему
    public void AddTeacher(Teacher teacher)
    {
        if (_teachers.Any(t => t.Id == teacher.Id))
        {
            throw new Exception("Преподаватель с таким ID уже есть в системе");
        }
        _teachers.Add(teacher);
    }

    public void AddStudent(Student student)
    {
        if (_students.Any(s => s.Id == student.Id))
        {
            throw new Exception("Студент с таким ID уже есть в системе");
        }
        _students.Add(student);
    }

    // Получение всех преподавателей и студентов в системе
    public IEnumerable<Teacher> GetAllTeachers()
    {
        return _teachers.AsReadOnly();
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return _students.AsReadOnly();
    }
}