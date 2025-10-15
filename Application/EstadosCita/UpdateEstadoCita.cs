
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosCita;

public sealed record UpdateEstadoCita(
    IdVO Id,
    NombreVO Nombre
) : IRequest<bool>;
