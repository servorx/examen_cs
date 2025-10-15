namespace Domain.ValueObjects;

public record FechaCitaVO
{
    public DateTime Value { get; }

    public FechaCitaVO(DateTime value)
    {
        // Validar formato y rango
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de cita no puede estar en el pasado.");

        // Normalizamos la fecha para evitar problemas de zonas horarias
        Value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd HH:mm:ss");
}
