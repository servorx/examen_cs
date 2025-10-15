using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Facturas;

public class CreateFacturaHandler : IRequestHandler<CreateFactura, IdVO>
{
    private readonly IFacturaRepository _repository;

    public CreateFacturaHandler(IFacturaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateFactura request, CancellationToken cancellationToken)
    {
        var factura = new Factura(
            id: IdVO.CreateNew(),
            ordenServicioId: request.OrdenServicioId,
            montoRepuestos: request.MontoRepuestos,
            manoObra: request.ManoObra,
            total: request.Total,
            fechaGeneracion: request.FechaGeneracion
        );

        await _repository.AddAsync(factura, cancellationToken);
        return factura.Id;
    }
}
