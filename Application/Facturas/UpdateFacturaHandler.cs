using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Facturas;

public class UpdateFacturaHandler : IRequestHandler<UpdateFactura, bool>
{
    private readonly IFacturaRepository _repository;

    public UpdateFacturaHandler(IFacturaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateFactura request, CancellationToken cancellationToken)
    {
        var factura = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (factura is null)
            return false;

        factura.OrdenServicioId = request.OrdenServicioId;
        factura.MontoRepuestos = request.MontoRepuestos;
        factura.ManoObra = request.ManoObra;
        factura.Total = request.Total;
        factura.FechaGeneracion = request.FechaGeneracion;

        return await _repository.UpdateAsync(factura, cancellationToken);
    }
}
