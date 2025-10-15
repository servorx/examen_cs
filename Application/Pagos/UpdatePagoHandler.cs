using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Pagos;

public class UpdatePagoHandler : IRequestHandler<UpdatePago, bool>
{
    private readonly IPagoRepository _repository;

    public UpdatePagoHandler(IPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdatePago request, CancellationToken cancellationToken)
    {
        var pago = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (pago is null)
            return false;

        pago.Factura = new Factura { Id = request.FacturaId };
        pago.MetodoPago = new MetodoPago { Id = request.MetodoPagoId };
        pago.EstadoPago = new EstadoPago { Id = request.EstadoPagoId };
        pago.Monto = request.Monto;
        pago.FechaPago = request.FechaPago;

        return await _repository.UpdateAsync(pago, cancellationToken);
    }
}
