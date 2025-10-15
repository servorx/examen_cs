using FluentValidation;

namespace Application.EstadosPago;

public class UpdateEstadoPagoValidator : AbstractValidator<UpdateEstadoPago>
{
    public UpdateEstadoPagoValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre no puede ser nulo");
    }
}
