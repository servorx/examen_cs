using Domain.ValueObjects;
using MediatR;

namespace Application.Mecanicos;

public sealed record UpdateMecanico(
    IdVO Id,
    NombreVO Nombre,
    TelefonoVO? Telefono,
    EspecialidadVO? Especialidad,
    EstadoVO IsActive
) : IRequest<bool>;
