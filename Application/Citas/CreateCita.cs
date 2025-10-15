using Domain.ValueObjects;
using MediatR;

namespace Application.Citas;

public sealed record CreateCita(
    IdVO ClienteId,
    IdVO VehiculoId,
    FechaCitaVO FechaCita,
    DescripcionVO? Motivo,
    IdVO EstadoId
) : IRequest<IdVO>;
