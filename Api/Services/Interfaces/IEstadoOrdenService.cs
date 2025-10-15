using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces
{
    public interface IEstadoOrdenService
    {
        Task<EstadoOrden> CrearAsync(EstadoOrden estadoOrden, CancellationToken ct = default);
        Task<bool> ActualizarAsync(EstadoOrden estadoOrden, CancellationToken ct = default);
        Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoOrden?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoOrden?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default);
        Task<IReadOnlyList<EstadoOrden>> ObtenerTodosAsync(CancellationToken ct = default);
    }
}
