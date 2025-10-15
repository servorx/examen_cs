using FluentValidation;

namespace Application.TipoMovimientos;

public class CreateTipoMovimientoValidator : AbstractValidator<CreateTipoMovimiento>
{
    public CreateTipoMovimientoValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre del tipo de movimiento es obligatorio.");
    }
}
