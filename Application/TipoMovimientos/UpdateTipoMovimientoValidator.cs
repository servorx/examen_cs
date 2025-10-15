using FluentValidation;

namespace Application.TipoMovimientos;

public class UpdateTipoMovimientoValidator : AbstractValidator<UpdateTipoMovimiento>
{
    public UpdateTipoMovimientoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre del tipo de movimiento es obligatorio.");
    }
}
