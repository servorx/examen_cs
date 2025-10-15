using Domain.ValueObjects;
using MediatR;

namespace Application.TipoMovimientos;

public sealed record UpdateTipoMovimiento(
    IdVO Id,
    NombreVO Nombre
) : IRequest<bool>;
