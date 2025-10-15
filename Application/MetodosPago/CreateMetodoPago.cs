using Domain.ValueObjects;
using MediatR;

namespace Application.MetodosPago;

public sealed record CreateMetodoPago(
    NombreVO Nombre
) : IRequest<IdVO>;
