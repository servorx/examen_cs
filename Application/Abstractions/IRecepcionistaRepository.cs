using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IRecepcionistaRepository 
{
    // metodos de lectura
    Task<Recepcionista?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Recepcionista>> GetAllAsync(CancellationToken ct = default);

    // devolver false o tru
    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    // metodos de insercion, actualizacion y eliminacion
    Task<int> AddAsync(Recepcionista admin, CancellationToken ct = default);
    Task<bool> UpdateAsync(Recepcionista admin, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
