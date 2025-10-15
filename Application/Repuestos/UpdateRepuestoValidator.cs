using FluentValidation;

namespace Application.Repuestos;

public class UpdateRepuestoValidator : AbstractValidator<UpdateRepuesto>
{
    public UpdateRepuestoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Codigo).NotNull();
        RuleFor(x => x.Descripcion).NotNull();
        RuleFor(x => x.CantidadStock).NotNull();
        RuleFor(x => x.PrecioUnitario).NotNull();
        // ProveedorId es opcional
    }
}
