using Domain.ValueObjects;
using MediatR;

namespace Application.Repuestos;

public sealed record CreateRepuesto(
    CodigoRepuestoVO Codigo,
    DescripcionVO Descripcion,
    CantidadVO CantidadStock,
    DineroVO PrecioUnitario,
    IdVO? ProveedorId
) : IRequest<IdVO>;
