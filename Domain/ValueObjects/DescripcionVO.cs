namespace Domain.ValueObjects;

public record DescripcionVO
{
    public string Value { get; }

    public DescripcionVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La descripción no puede estar vacía.");
        if (value.Length > 255)
            throw new ArgumentException("La descripción no puede exceder los 255 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
