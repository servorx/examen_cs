using Domain.ValueObjects;
using MediatR;

namespace Application.MetodosPago;

public sealed record UpdateMetodoPago(
    IdVO Id,
    NombreVO Nombre
) : IRequest<bool>;
