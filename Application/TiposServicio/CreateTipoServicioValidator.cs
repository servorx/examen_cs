using FluentValidation;

namespace Application.TipoServicios;

public class CreateTipoServicioValidator : AbstractValidator<CreateTipoServicio>
{
    public CreateTipoServicioValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre del servicio es obligatorio.");
        RuleFor(x => x.Descripcion).NotNull().WithMessage("La descripción es obligatoria.");
        RuleFor(x => x.PrecioBase).NotNull().WithMessage("El precio base es obligatorio.");
    }
}
