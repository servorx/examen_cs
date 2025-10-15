using FluentValidation;

namespace Application.MetodosPago;

public class UpdateMetodoPagoValidator : AbstractValidator<UpdateMetodoPago>
{
    public UpdateMetodoPagoValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("El Id es obligatorio");
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es obligatorio");
    }
}
