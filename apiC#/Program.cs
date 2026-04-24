var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

//Usar middleware
app.UseCors();

//PRUEBAS (ruta base)
app.MapGet("/", () => Results.Json(new { message = "¡Bienvenido a la API REST!", version = "1.0.0" }));

// USERS (Registrar las rutas de usuario)
app.MapUserRoutes();

// Manejo de errores para rutas no encontradas (404)
app.MapFallback(() => Results.NotFound(new { error = "Ruta no encontrada" }));

//Manejo de errores para rutas no encontradas (404)
app.MapFallback(() => Results.NotFound(new { error = "Ruta no encontrada" }));

app.Run();