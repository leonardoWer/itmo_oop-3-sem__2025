namespace Lab1.Models;

public class Admin
{
    public string Login { get; }
    public string Password { get; set; }

    public Admin(string login, string password)
    {
        Login = login;
        Password = password;
    }
}