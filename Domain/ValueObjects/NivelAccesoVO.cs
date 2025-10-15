namespace Domain.ValueObjects;

public record NivelAccesoVO
{
    public string Value { get; }

    public NivelAccesoVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nivel de acceso no puede estar vacío.");
        if (value.Length > 50)
            throw new ArgumentException("El nivel de acceso no puede exceder los 50 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
