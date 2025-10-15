using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Repuestos;

public class UpdateRepuestoHandler : IRequestHandler<UpdateRepuesto, bool>
{
    private readonly IRepuestoRepository _repository;

    public UpdateRepuestoHandler(IRepuestoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateRepuesto request, CancellationToken cancellationToken)
    {
        var repuesto = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (repuesto is null)
            return false;

        repuesto.Codigo = request.Codigo;
        repuesto.Descripcion = request.Descripcion;
        repuesto.CantidadStock = request.CantidadStock;
        repuesto.PrecioUnitario = request.PrecioUnitario;
        repuesto.ProveedorId = request.ProveedorId;

        return await _repository.UpdateAsync(repuesto, cancellationToken);
    }
}
