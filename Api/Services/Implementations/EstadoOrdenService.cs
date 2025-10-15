using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations
{
    public class EstadoOrdenService : IEstadoOrdenService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstadoOrdenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EstadoOrden> CrearAsync(EstadoOrden estadoOrden, CancellationToken ct = default)
        {
            var existente = await _unitOfWork.EstadoOrden.GetByNombreAsync(estadoOrden.Nombre, ct);
            if (existente != null)
                throw new Exception($"Ya existe un estado de orden con el nombre '{estadoOrden.Nombre.Value}'.");

            return await _unitOfWork.EstadoOrden.AddAsync(estadoOrden, ct);
        }

        public async Task<bool> ActualizarAsync(EstadoOrden estadoOrden, CancellationToken ct = default)
        {
            var existente = await _unitOfWork.EstadoOrden.GetByIdAsync(estadoOrden.Id, ct);
            if (existente == null)
                throw new Exception("El estado de orden no existe.");

            var duplicado = await _unitOfWork.EstadoOrden.GetByNombreAsync(estadoOrden.Nombre, ct);
            if (duplicado != null && duplicado.Id.Value != estadoOrden.Id.Value)
                throw new Exception($"Ya existe otro estado con el nombre '{estadoOrden.Nombre.Value}'.");

            return await _unitOfWork.EstadoOrden.UpdateAsync(estadoOrden, ct);
        }

        public async Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoOrden.DeleteAsync(id, ct);
        }

        public async Task<EstadoOrden?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoOrden.GetByIdAsync(id, ct);
        }

        public async Task<EstadoOrden?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoOrden.GetByNombreAsync(nombre, ct);
        }

        public async Task<IReadOnlyList<EstadoOrden>> ObtenerTodosAsync(CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoOrden.GetAllAsync(ct);
        }
    }
}
