using Domain.ValueObjects;
using MediatR;

namespace Application.Administradores;

public sealed record UpdateAdministrador(
    IdVO Id,
    NombreVO Nombre,
    TelefonoVO Telefono,
    NivelAccesoVO NivelAcceso,
    DescripcionVO AreaResponsabilidad,
    EstadoVO IsActive
) : IRequest<bool>;
