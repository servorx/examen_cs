namespace Domain.ValueObjects;

public record CantidadVO
{
    public int Value { get; }

    public CantidadVO(int value)
    {
        if (value < 0)
            throw new ArgumentException("La cantidad no puede ser negativa.");
        if (value > 999999999)
            throw new ArgumentException("La cantidad excede el límite permitido.");
        Value = value;
    }

    public override string ToString() => Value.ToString();
}
