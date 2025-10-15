namespace Domain.ValueObjects;

public record CodigoRepuestoVO
{
    public string Value { get; }

    public CodigoRepuestoVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código del repuesto no puede estar vacío.");
        if (value.Length > 50)
            throw new ArgumentException("El código del repuesto no puede exceder los 50 caracteres.");

        Value = value.Trim().ToUpperInvariant();
    }

    public override string ToString() => Value;
}
