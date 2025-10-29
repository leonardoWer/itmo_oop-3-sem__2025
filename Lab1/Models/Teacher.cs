using System;
using System.Collections.Generic;
using Lab1.Models.Abstract;

namespace Lab1.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
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