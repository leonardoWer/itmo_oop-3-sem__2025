using System;
using Lab1.Models.Abstract;

namespace Lab1.Models;

public class OfflineCourse : Course
{
    public string Location { get; set; }
    public DateTime Schedule { get; set; }
    
    public OfflineCourse(int id, string title, string descr, string location, DateTime schedule)
        : base(id, title, descr)
    {
        Location = location;
        Schedule = schedule;
    }
    
    public OfflineCourse(int id, string title, string location, DateTime schedule)
        : this(id, title, "",  location, schedule)
    {}
    
    public override string GetCourseDetails()
    {
        return $"Онлайн {base.ToString()}: Локация {Location}, Расписание - {Schedule}";
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Тип: Очно | {GetCourseDetails()}";
    }
}