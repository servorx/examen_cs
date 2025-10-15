namespace Domain.ValueObjects;

public record KilometrajeVO
{
    public int Value { get; }

    public KilometrajeVO(int value)
    {
        if (value < 0)
            throw new ArgumentException("El kilometraje no puede ser negativo.");
        Value = value;
        if (value > 999999999)
            throw new ArgumentException("El kilometraje excede el límite permitido.");
    }

    public override string ToString() => $"{Value:N0} km";
}
