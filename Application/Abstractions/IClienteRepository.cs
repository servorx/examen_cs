using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Cliente>> GetAllAsync(CancellationToken ct = default);
    Task<Cliente?> GetByUserIdAsync(IdVO userId, CancellationToken ct = default); // para login o referencias

    // devolver false o tru 
    Task<bool> ExistsByEmailAsync(CorreoVO correo, CancellationToken ct = default);
    Task<int> AddAsync(Cliente cliente, CancellationToken ct = default);
    Task<bool> UpdateAsync(Cliente cliente, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
