using FluentValidation;

namespace Application.MetodosPago;

public class CreateMetodoPagoValidator : AbstractValidator<CreateMetodoPago>
{
    public CreateMetodoPagoValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es obligatorio");
    }
}
