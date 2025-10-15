using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IAdministradorService
{
    Task<Administrador?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Administrador>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Administrador>> GetActiveAsync(CancellationToken ct = default);

    Task<int> AddAsync(Administrador administrador, CancellationToken ct = default);
    Task<bool> UpdateAsync(Administrador administrador, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
}
