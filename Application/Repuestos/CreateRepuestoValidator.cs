using FluentValidation;

namespace Application.Repuestos;

public class CreateRepuestoValidator : AbstractValidator<CreateRepuesto>
{
    public CreateRepuestoValidator()
    {
        RuleFor(x => x.Codigo).NotNull();
        RuleFor(x => x.Descripcion).NotNull();
        RuleFor(x => x.CantidadStock).NotNull();
        RuleFor(x => x.PrecioUnitario).NotNull();
        // ProveedorId es opcional
    }
}
