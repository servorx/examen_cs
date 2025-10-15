using Application.Abstractions.Auth;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Auth;

public class UserMemberRolRepository(AppDbContext db) : IUserMemberRolService
{
    public async Task<IEnumerable<UserMemberRol>> GetAllAsync()
    {
        return await db.UserMemberRols
            .AsNoTracking()
            .Include(umr => umr.UserMembers)
            .Include(umr => umr.Rol)
            .ToListAsync();
    }

    public async Task<UserMemberRol?> GetByIdsAsync(int userMemberId, int roleId)
    {
        return await db.UserMemberRols
            .Include(umr => umr.UserMembers)
            .Include(umr => umr.Rol)
            .FirstOrDefaultAsync(umr => umr.UserMemberId == userMemberId && umr.RolId == roleId);
    }

    public void Remove(UserMemberRol entity)
    {
        db.UserMemberRols.Remove(entity);
    }

    public void Update(UserMemberRol entity)
    {
        db.UserMemberRols.Update(entity);
    }
}
