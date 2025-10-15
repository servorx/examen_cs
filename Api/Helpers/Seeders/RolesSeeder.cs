using Domain.Entities.Auth;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class RolesSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        var existingNames = await db.Roles.Select(r => r.Name).ToListAsync();
        var targetNames = Enum.GetNames(typeof(UserAuthorization.Roles));

        var toAdd = targetNames
            .Except(existingNames, StringComparer.OrdinalIgnoreCase)
            .Select(name => new Rol
            {
                Name = name,
                Description = $"{name} role"
            })
            .ToList();

        if (toAdd.Count > 0)
        {
            db.Roles.AddRange(toAdd);
            await db.SaveChangesAsync();
        }
    }
}
