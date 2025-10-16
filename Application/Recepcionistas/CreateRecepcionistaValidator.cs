using FluentValidation;
using Application.Recepcionistas;

namespace Application.Recepcionistas;
public class CreateRecepcionistaValidator : AbstractValidator<CreateRecepcionista>
{
    public CreateRecepcionistaValidator()
    {
        RuleFor(x => x.Nombre).NotNull();
        RuleFor(x => x.Telefono).NotNull();
        RuleFor(x => x.AnioExperiencia).NotNull();
        RuleFor(x => x.IsActive).NotNull();
    }
}

