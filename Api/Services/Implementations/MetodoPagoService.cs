using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MetodoPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CrearAsync(MetodoPago metodoPago, CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.AddAsync(metodoPago, ct);
        }

        public async Task<bool> ActualizarAsync(MetodoPago metodoPago, CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.UpdateAsync(metodoPago, ct);
        }

        public async Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.DeleteAsync(id, ct);
        }

        public async Task<MetodoPago?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.GetByIdAsync(id, ct);
        }

        public async Task<MetodoPago?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.GetByNombreAsync(nombre, ct);
        }

        public async Task<IReadOnlyList<MetodoPago>> ObtenerTodosAsync(CancellationToken ct = default)
        {
            return await _unitOfWork.MetodoPago.GetAllAsync(ct);
        }
    }
}
