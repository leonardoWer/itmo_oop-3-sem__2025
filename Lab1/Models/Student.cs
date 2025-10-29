namespace Lab1.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public Student(int id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }
    
    public Student(int id, string name)
        : this(id, name, "")
    {}

    public override string ToString()
    {
        return $"[{Id}] {Name} {Surname}";
    }
}