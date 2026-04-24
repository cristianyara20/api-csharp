var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// AGREGAR SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// USAR SWAGGER
app.UseSwagger();
app.UseSwaggerUI();

// Usar middleware
app.UseCors();

// PRUEBAS (ruta base)
app.MapGet("/", () => Results.Json(new
{
    message = "¡Bienvenido a la API REST!",
    version = "1.0.0"
}));

// CRUD de usuarios
app.MapGet("/api/users", MIApiRestCsharp.Controllers.UserController.GetAllUsers);
app.MapGet("/api/users/{id}", MIApiRestCsharp.Controllers.UserController.GetUserById);
app.MapPost("/api/users", MIApiRestCsharp.Controllers.UserController.CreateUser);
app.MapPut("/api/users/{id}", MIApiRestCsharp.Controllers.UserController.UpdateUser);
app.MapDelete("/api/users/{id}", MIApiRestCsharp.Controllers.UserController.DeleteUser);

// Manejo de errores para rutas no encontradas (404)
app.MapFallback(() => Results.NotFound(new
{
    error = "Ruta no encontrada"
}));

app.Run();