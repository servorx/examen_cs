using FluentValidation;

namespace Application.Mecanicos;

public class CreateMecanicoValidator : AbstractValidator<CreateMecanico>
{
    public CreateMecanicoValidator()
    {
        RuleFor(x => x.Nombre).NotNull().WithMessage("El nombre es obligatorio");
        RuleFor(x => x.IsActive).NotNull().WithMessage("El estado activo es obligatorio");
    }
}
