using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.HistorialesInventario;

public class UpdateHistorialInventarioHandler : IRequestHandler<UpdateHistorialInventario, bool>
{
    private readonly IHistorialInventarioRepository _repository;

    public UpdateHistorialInventarioHandler(IHistorialInventarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateHistorialInventario request, CancellationToken cancellationToken)
    {
        var historial = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (historial is null)
            return false;

        historial.RepuestoId = request.RepuestoId;
        historial.AdminId = request.AdminId;
        historial.TipoMovimientoId = request.TipoMovimientoId;
        historial.Cantidad = request.Cantidad;
        historial.FechaMovimiento = request.FechaMovimiento;
        historial.Observaciones = request.Observaciones;

        return await _repository.UpdateAsync(historial, cancellationToken);
    }
}
