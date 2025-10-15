
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosOrden;

public sealed record UpdateEstadoOrden(
    IdVO Id,
    NombreVO Nombre
) : IRequest<bool>;
