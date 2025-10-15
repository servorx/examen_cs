namespace Domain.ValueObjects;

public record DireccionVO
{
    public string Value { get; }

    public DireccionVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La dirección no puede estar vacía.");
        if (value.Length < 5) 
            throw new ArgumentException("La dirección debe tener al menos 5 caracteres.");
        if (value.Length > 255)
            throw new ArgumentException("La dirección no puede exceder los 255 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
