namespace Domain.ValueObjects;

public record AnioVehiculoVO
{
    public int Value { get; }

    public AnioVehiculoVO(int value)
    {
        int currentYear = DateTime.UtcNow.Year + 1;
        if (value < 1950 || value > currentYear)
            throw new ArgumentException($"El año del vehículo debe estar entre 1950 y {currentYear}.");
        Value = value;
    }

    public override string ToString() => Value.ToString();
}
