namespace Domain.ValueObjects;

public record FechaHistoricaVO
{
    public DateTime Value { get; }

    public FechaHistoricaVO(DateTime value)
    {
        // Validar rango razonable (no más de 50 años atrás o 10 años al futuro, por ejemplo)
        if (value < DateTime.UtcNow.AddYears(-50))
            throw new ArgumentException("La fecha no puede ser anterior a hace 50 años.");

        if (value > DateTime.UtcNow.AddYears(10))
            throw new ArgumentException("La fecha no puede ser demasiado lejana en el futuro.");

        Value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd HH:mm:ss");
}
