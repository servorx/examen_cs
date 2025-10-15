using Domain.Entities.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class UsersSeeder
{
    public static async Task SeedBaseUsersAsync(AppDbContext db)
    {
        var existingUsernames = await db.UsersMembers
            .Select(u => u.Username)
            .ToListAsync();

        var users = new List<UserMember>();

        void AddIfNotExists(string username, string email, string password)
        {
            if (!existingUsernames.Contains(username))
            {
                users.Add(new UserMember
                {
                    Username = username,
                    Email = email,
                    Password = password
                });
            }
        }

        AddIfNotExists("admin1", "admin@taller.com", "admin123");
        AddIfNotExists("cliente1", "cliente@correo.com", "cliente123");
        AddIfNotExists("mecanico1", "mecanico@correo.com", "mecanico123");
        AddIfNotExists("proveedor1", "proveedor@correo.com", "proveedor123");

        if (users.Count > 0)
        {
            db.UsersMembers.AddRange(users);
            await db.SaveChangesAsync();
        }
    }
}
