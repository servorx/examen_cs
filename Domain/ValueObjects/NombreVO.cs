namespace Domain.ValueObjects;

public record NombreVO
{
    public string Value { get; }

    public NombreVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (value.Length < 2)
            throw new ArgumentException("El nombre debe tener al menos 2 caracteres.");
        if (value.Length > 100)
            throw new ArgumentException("El nombre no puede exceder los 100 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
