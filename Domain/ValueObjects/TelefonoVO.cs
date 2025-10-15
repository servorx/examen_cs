namespace Domain.ValueObjects;

public record TelefonoVO
{
    public string Value { get; }

    public TelefonoVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El teléfono no puede estar vacío.");

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[0-9+\-\s]{7,20}$"))
            throw new ArgumentException("El teléfono tiene un formato inválido.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
