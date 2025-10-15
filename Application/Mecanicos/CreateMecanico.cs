using Domain.ValueObjects;
using MediatR;

namespace Application.Mecanicos;

public sealed record CreateMecanico(
    NombreVO Nombre,
    TelefonoVO? Telefono,
    EspecialidadVO? Especialidad,
    EstadoVO IsActive,
    int UserId
) : IRequest<IdVO>;
