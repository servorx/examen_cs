using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Repuestos;

public class CreateRepuestoHandler : IRequestHandler<CreateRepuesto, IdVO>
{
    private readonly IRepuestoRepository _repository;

    public CreateRepuestoHandler(IRepuestoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateRepuesto request, CancellationToken cancellationToken)
    {
        var repuesto = new Repuesto(
            codigo: request.Codigo,
            descripcion: request.Descripcion,
            cantidadStock: request.CantidadStock,
            precioUnitario: request.PrecioUnitario,
            proveedorId: request.ProveedorId
        );

        // Generamos un nuevo IdVO
        repuesto.Id = IdVO.CreateNew();

        await _repository.AddAsync(repuesto, cancellationToken);
        return repuesto.Id;
    }
}
