
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosCita;

public sealed record CreateEstadoCita(
    NombreVO Nombre
) : IRequest<IdVO>;
