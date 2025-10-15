namespace Domain.ValueObjects;

public record EspecialidadVO
{
    public string Value { get; }

    public EspecialidadVO(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La especialidad no puede estar vacía.");
        if (value.Length > 60)
            throw new ArgumentException("La especialidad no puede exceder los 60 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
