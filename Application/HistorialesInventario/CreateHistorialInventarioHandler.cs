using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.HistorialesInventario;

public class CreateHistorialInventarioHandler : IRequestHandler<CreateHistorialInventario, IdVO>
{
    private readonly IHistorialInventarioRepository _repository;

    public CreateHistorialInventarioHandler(IHistorialInventarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateHistorialInventario request, CancellationToken cancellationToken)
    {
        var historial = new HistorialInventario(
            id: IdVO.CreateNew(),
            repuestoId: request.RepuestoId,
            adminId: request.AdminId,
            tipoMovimientoId: request.TipoMovimientoId,
            cantidad: request.Cantidad,
            fechaMovimiento: request.FechaMovimiento,
            observaciones: request.Observaciones
        );

        await _repository.AddAsync(historial, cancellationToken);
        return historial.Id;
    }
}
