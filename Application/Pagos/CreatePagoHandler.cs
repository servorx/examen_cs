using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Pagos;

public class CreatePagoHandler : IRequestHandler<CreatePago, IdVO>
{
    private readonly IPagoRepository _repository;

    public CreatePagoHandler(IPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreatePago request, CancellationToken cancellationToken)
    {
        var pago = new Pago(
            id: IdVO.CreateNew(),
            factura: new Factura { Id = request.FacturaId },
            metodoPago: new MetodoPago { Id = request.MetodoPagoId },
            estadoPago: new EstadoPago { Id = request.EstadoPagoId },
            monto: request.Monto,
            fechaPago: request.FechaPago
        );

        await _repository.AddAsync(pago, cancellationToken);
        return pago.Id;
    }
}
