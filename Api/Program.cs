using Api.Extensions;
using Api.Mappings;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomRateLimiter();

// 🔸 Swagger con configuración JWT integrada
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AutoTallerManager API",
        Version = "v1",
        Description = "Sistema de gestión integral de taller automotriz"
    });

    // Configuración de seguridad JWT en Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando el esquema Bearer.
                        Escribe **'Bearer {tu_token}'** en el campo de abajo.
                        Ejemplo: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...`",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.ConfigureCors();
builder.Services.AddApplicationServices();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddValidationErrors();

// Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    string connectionString = builder.Configuration.GetConnectionString(isDocker ? "PostgresDocker" : "PostgresLocal")!;
    options.UseNpgsql(connectionString)
            .EnableDetailedErrors() // muestra excepciones detalladas
            .EnableSensitiveDataLogging() // 🔍 muestra valores reales en las queries
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

// Muestra la conexión a la base de datos de Postgres
Console.WriteLine(builder.Configuration.GetConnectionString("Postgres"));

// Swagger y middlewares
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoTallerManager API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

// Esto es para agregar los Seeders de la base de datos
await app.SeedDatabaseAsync();

// Agregar CORS y RateLimiter
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseRateLimiter();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        Console.WriteLine("❌ Token inválido o no autorizado:");
        Console.WriteLine($"Path: {context.Request.Path}");
        Console.WriteLine($"Auth Header: {context.Request.Headers["Authorization"]}");
    }
});
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
