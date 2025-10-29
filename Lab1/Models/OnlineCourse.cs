using Lab1.Models.Abstract;

namespace Lab1.Models;

public class OnlineCourse : Course
{
    private string Platform {get; set;}
    private string Url {get; set;}

    public OnlineCourse(int id, string title, string descr,
        string platform, string accessUrl)
        : base(id, title, descr)
    {
        Platform = platform;
        Url = accessUrl;
    }
    
    public OnlineCourse(int id, string title,
        string platform, string accessUrl)
        : this(id, title, "", platform, accessUrl)
    {}
    
    public override string GetCourseDetails()
    {
        return $"Онлайн {base.ToString()}: Платформа - {Platform}, Ссылка на занятия - {Url}";
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Тип: Online | {GetCourseDetails()}";
    }
    
}