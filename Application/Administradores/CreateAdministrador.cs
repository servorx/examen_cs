using Domain.ValueObjects;
using MediatR;

namespace Application.Administradores;

public sealed record CreateAdministrador(
    NombreVO Nombre,
    TelefonoVO Telefono,
    NivelAccesoVO NivelAcceso,
    DescripcionVO AreaResponsabilidad,
    EstadoVO IsActive,
    int UserId
) : IRequest<IdVO>;

