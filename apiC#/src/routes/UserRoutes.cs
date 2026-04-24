using MIApiRestCsharp.Controllers;

namespace MiApiRestCsharp.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/users");

        group.MapGet("/", UserController.GetAllUsers);
        group.MapGet("/{id:int}", UserController.GetUserById);
        group.MapPost("/", UserController.CreateUser);
        group.MapPut("/{id:int}", UserController.UpdateUser);
        group.MapDelete("/{id:int}", UserController.DeleteUser);
    }
}
