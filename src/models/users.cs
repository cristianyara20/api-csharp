namespace MIApiRestCsharp.Models;

public class User
{
    public int Id {get; set;}
    public string Name {get; set; } = string.Empty;
    public string Email {get; set; } = string.Empty;

    public int Age {get; set; }
}

public static class UserMockDB
{
    public static List<User> Users = new List<User>
    {
        new User {Id = 1, Name = "Juan García", Email = "juan@sena.edu.co", Age = 28},
        new User {Id = 2, Name = "Maria López", Email = "maria@sena.edu.co", Age = 32},
        new User {Id = 3, Name = "Carlos Rodríguez", Email = "carlos@sena.edu.co", Age = 25}
    };

}