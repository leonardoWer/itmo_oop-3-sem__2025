namespace Lab1.Models.Abstract;

using System.Collections.Generic;
using System;

public abstract class Course
{
    public int Id { get; set; }
    private string Title { get; set; }
    private string Description { get; set; }
    public List<Student> EnrolledStudents { get; set; } = new List<Student>();
    public Teacher? AssignedTeacher { get; set; }

    public Course(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public Course(int id, string title)
        : this(id, title, ""){}

    public void ClearCourse()
    {
        AssignedTeacher?.CoursesTaught.Remove(this);
        EnrolledStudents.Clear();
    }
    
    // Описание специфики курса
    public virtual string GetCourseDetails()
    {
        return $"Abstract Course Details";
    }

    public override string ToString()
    {
        string teacherInfo = AssignedTeacher == null ? "Преподаватель не назначен" : AssignedTeacher.GetFIO();
        return $"Курс [{Id}]: {Title}; Преподаватель: {teacherInfo} | Количество учащихся: {EnrolledStudents.Count}";
    }
}