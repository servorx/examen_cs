using FluentValidation;
using Application.Administradores;

namespace Application.Administradores;
public class CreateAdministradorValidator : AbstractValidator<CreateAdministrador>
{
    public CreateAdministradorValidator()
    {
        RuleFor(x => x.Nombre).NotNull();
        RuleFor(x => x.Telefono).NotNull();
        RuleFor(x => x.NivelAcceso).NotNull();
        RuleFor(x => x.AreaResponsabilidad).NotNull();
        RuleFor(x => x.IsActive).NotNull();
    }
}

