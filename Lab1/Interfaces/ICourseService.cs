namespace Lab1.Interfaces;

using System.Collections.Generic;
using Lab1.Models;
using Lab1.Models.Abstract;

// Определяет операции над курсами
public interface ICourseService
{
    // Управление курсами
    void AddCourse(Course course);
    bool RemoveCourse(int courseId);
    Course? GetCourseById(int courseId);
    IEnumerable<Course> GetAllCourses();
    IEnumerable<Course> GetCoursesByTeacher(int teacherId);
    
    // Назначение преподавателей
    bool AssignTeacherToCourse(int courseId, int teacherId);
    bool UnassignTeacherFromCourse(int courseId);
    
    // Управление студентами
    bool AddStudentToCourse(int courseId, int studentId);
    bool RemoveStudentFromCourse(int courseId, int studentId);
    IEnumerable<Student> GetStudentsOnCourse(int courseId);
}