using Domain.ValueObjects;
using MediatR;

namespace Application.Citas;

public sealed record UpdateCita(
    IdVO Id,
    IdVO ClienteId,
    IdVO VehiculoId,
    FechaCitaVO FechaCita,
    DescripcionVO? Motivo,
    IdVO EstadoId
) : IRequest<bool>;
