using Microsoft.AspNetCore.Mvc;
using MIApiRestCsharp.Models;

namespace MIApiRestCsharp.Controllers;

public static class UserController
{
    public static IResult GetAllUsers() => Results.Json(new { success = true, data = UserMockDB.Users, total = UserMockDB.Users.Count });

    public static IResult GetUserById(int id)
    {
        var user = UserMockDB.Users.FirstOrDefault(u => u.Id == id);
        return user == null
            ? Results.NotFound(new { success = false, error = $"Usuario con ID {id} no encontrado" })
            : Results.Json(new { success = true, data = user });
    }

    public static IResult CreateUser([FromBody] UserCreateDto dto)
    {
        if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) || dto.Age <= 0)
            return Results.BadRequest(new { success = false, error = "Los campos son obligatorios" });

        var newId = UserMockDB.Users.Count > 0 ? UserMockDB.Users.Max(u => u.Id) + 1 : 1;
        var newUser = new User { Id = newId, Name = dto.Name, Email = dto.Email, Age = dto.Age };
        UserMockDB.Users.Add(newUser);

        return Results.Created($"/api/users/{newId}", new { success = true, message = "Usuario creado", data = newUser });
    }

    public static IResult UpdateUser(int id, [FromBody] UserUpdateDto dto)
    {
        var user = UserMockDB.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return Results.NotFound(new { success = false, error = "No encontrado" });

        if (!string.IsNullOrEmpty(dto.Name)) user.Name = dto.Name;
        if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
        if (dto.Age.HasValue) user.Age = dto.Age.Value;

        return Results.Json(new { success = true, message = "Actualizado", data = user });
    }

    public static IResult DeleteUser(int id)
    {
        var user = UserMockDB.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return Results.NotFound(new { success = false, error = "No encontrado" });

        UserMockDB.Users.Remove(user);
        return Results.Json(new { success = true, message = "Eliminado", data = user });
    }
}

public class UserCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
}

public class UserUpdateDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }
}
