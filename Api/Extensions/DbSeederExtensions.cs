using Api.Helpers.Seeders;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class DbSeederExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Database.MigrateAsync();

        await RolesSeeder.SeedAsync(db);
        await UsersSeeder.SeedBaseUsersAsync(db);
        await ExtendedUsersSeeder.SeedAsync(db);
        await UserRolesSeeder.SeedAsync(db);
        await CatalogsSeeder.SeedAsync(db);
        await VehiculosSeeder.SeedAsync(db);
        await RepuestosSeeder.SeedAsync(db);
        await OrdenesYFacturasSeeder.SeedAsync(db);
        await HistorialInventarioSeeder.SeedAsync(db); // historial de inventario
        await CitasSeeder.SeedAsync(db);
        await FacturasSeeder.SeedAsync(db);
        await PagosSeeder.SeedAsync(db);
    }
}

