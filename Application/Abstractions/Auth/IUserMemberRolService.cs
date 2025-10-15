using System;
using Domain.Entities.Auth;

namespace Application.Abstractions.Auth;

public interface IUserMemberRolService
{
        Task<IEnumerable<UserMemberRol>> GetAllAsync();
        void Remove(UserMemberRol entity);
        void Update(UserMemberRol entity);
        Task<UserMemberRol?> GetByIdsAsync(int userMemberId, int roleId);

}
