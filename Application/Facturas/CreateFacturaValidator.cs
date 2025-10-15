using FluentValidation;

namespace Application.Facturas;

public class CreateFacturaValidator : AbstractValidator<CreateFactura>
{
    public CreateFacturaValidator()
    {
        RuleFor(x => x.OrdenServicioId).NotNull().WithMessage("El Id del OrdenServicio es obligatorio");
        RuleFor(x => x.MontoRepuestos).NotNull().WithMessage("El monto de repuestos es obligatorio");
        RuleFor(x => x.ManoObra).NotNull().WithMessage("El costo de mano de obra es obligatorio");
        RuleFor(x => x.Total).NotNull().WithMessage("El total es obligatorio");
        RuleFor(x => x.FechaGeneracion).NotNull().WithMessage("La fecha de generación es obligatoria");
    }
}
