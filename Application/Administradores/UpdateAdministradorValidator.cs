using FluentValidation;

namespace Application.Administradores;

public class UpdateAdministradorValidator : AbstractValidator<UpdateAdministrador>
{
    public UpdateAdministradorValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Nombre).NotNull();
        RuleFor(x => x.Telefono).NotNull();
        RuleFor(x => x.NivelAcceso).NotNull();
        RuleFor(x => x.AreaResponsabilidad).NotNull();
        RuleFor(x => x.IsActive).NotNull();
    }
}
