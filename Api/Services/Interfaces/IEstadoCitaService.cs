using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces
{
    public interface IEstadoCitaService
    {
        Task<int> CrearAsync(EstadoCita estadoCita, CancellationToken ct = default);
        Task<bool> ActualizarAsync(EstadoCita estadoCita, CancellationToken ct = default);
        Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoCita?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoCita?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default);
        Task<IReadOnlyList<EstadoCita>> ObtenerTodosAsync(CancellationToken ct = default);
    }
}
