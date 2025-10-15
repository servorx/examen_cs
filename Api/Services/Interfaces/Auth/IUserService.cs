using System;
using Api.DTOs.Auth;

namespace Api.Services.Interfaces.Auth;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataUserDto> GetTokenAsync(LoginDto model, CancellationToken ct = default);

    Task<string> AddRoleAsync(AddRoleDto model);

    Task<DataUserDto> RefreshTokenAsync(string refreshToken);

    Task<DataUserDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<DataUserDto>> GetAllAsync(CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
