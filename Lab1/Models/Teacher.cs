using System;
using System.Collections.Generic;

namespace Lab1.Models;

public class Teacher
{
    private int Id { get; set; }
    private string Name { get; set; }
    private string Surname { get; set; }
    public List<Course> CoursesTaught { get; set; } = new List<Course>();
    
    public Teacher(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }

    public String GetFIO()
    {
        return $"{Name} {Surname}";
    }

    public override string ToString()
    {
        return $"[{Id}] {Name} {Surname}";
    }
}