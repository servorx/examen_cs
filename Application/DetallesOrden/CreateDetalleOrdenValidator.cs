using FluentValidation;

namespace Application.DetalleOrdenes;

public class CreateDetalleOrdenValidator : AbstractValidator<CreateDetalleOrden>
{
    public CreateDetalleOrdenValidator()
    {
        RuleFor(x => x.OrdenServicioId).NotNull();
        RuleFor(x => x.RepuestoId).NotNull();
        RuleFor(x => x.Cantidad).NotNull();
        RuleFor(x => x.Costo).NotNull();
    }
}
