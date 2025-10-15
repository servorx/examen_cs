namespace Domain.ValueObjects;

public record VinVO
{
    public string Value { get; }

    public VinVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El VIN no puede estar vacío.");
        if (value.Length != 17)
            throw new ArgumentException("El VIN debe tener exactamente 17 caracteres.");
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-HJ-NPR-Z0-9]+$"))
            throw new ArgumentException("El VIN contiene caracteres inválidos.");

        Value = value.ToUpperInvariant();
    }

    public override string ToString() => Value;
}
