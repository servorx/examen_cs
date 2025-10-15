using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IAdministradorRepository
{
    // metodos de lectura
    Task<Administrador?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Administrador>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Administrador>> GetByNivelAccesoAsync(NivelAccesoVO nivel, CancellationToken ct = default);

    // devolver false o tru
    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    // metodos de insercion, actualizacion y eliminacion
    Task<int> AddAsync(Administrador admin, CancellationToken ct = default);
    Task<bool> UpdateAsync(Administrador admin, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
