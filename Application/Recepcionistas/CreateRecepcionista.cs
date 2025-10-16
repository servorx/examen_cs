using Domain.ValueObjects;
using MediatR;

namespace Application.Recepcionistas;

public sealed record CreateRecepcionista(
    NombreVO Nombre,
    TelefonoVO Telefono,
    AnioExperienciaVO AnioExperiencia,
    EstadoVO IsActive,
    int UserId
) : IRequest<IdVO>;

