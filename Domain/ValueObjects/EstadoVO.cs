namespace Domain.ValueObjects;

public record EstadoVO
{
    public bool Value { get; }

    // 👇 EF Core necesita este constructor vacío
    private EstadoVO() { }

    public EstadoVO(bool value)
    {
        if (value != true && value != false)
            throw new ArgumentException("El estado debe ser verdadero o falso.");

        Value = value;
    }

    public bool EstaActivo() => Value;

    public override string ToString() => Value ? "Activo" : "Inactivo";

    public static implicit operator bool(EstadoVO estado) => estado.Value;
    public static implicit operator EstadoVO(bool value) => new EstadoVO(value);
}
