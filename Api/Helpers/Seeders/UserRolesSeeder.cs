using Domain.Entities.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class UserRolesSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.UserMemberRols.AnyAsync()) return;

        var users = await db.UsersMembers.ToListAsync();
        var roles = await db.Roles.ToListAsync();

        var userRoles = new List<UserMemberRol>
        {
            new UserMemberRol { UserMemberId = users.First(u => u.Username == "admin1").Id, RolId = roles.First(r => r.Name == "Administrador").Id },
            new UserMemberRol { UserMemberId = users.First(u => u.Username == "cliente1").Id, RolId = roles.First(r => r.Name == "Cliente").Id },
            new UserMemberRol { UserMemberId = users.First(u => u.Username == "mecanico1").Id, RolId = roles.First(r => r.Name == "Mecanico").Id },
            new UserMemberRol { UserMemberId = users.First(u => u.Username == "proveedor1").Id, RolId = roles.First(r => r.Name == "Proveedor").Id }
        };

        db.UserMemberRols.AddRange(userRoles);
        await db.SaveChangesAsync();
    }
}
