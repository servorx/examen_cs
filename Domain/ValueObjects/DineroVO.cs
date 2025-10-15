namespace Domain.ValueObjects;

public record DineroVO
{
    public decimal Value { get; }

    public DineroVO(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("El monto no puede ser negativo.");
        if (value > 999999999.99M)
            throw new ArgumentException("El monto excede el límite permitido.");

        Value = decimal.Round(value, 2);
    }

    // Operadores de suma y resta
    // permite que 2 numeros se sumen o restan como si fueran numeros de tipo DineroVO, manteniendo la regla del dominio
    public static DineroVO operator +(DineroVO a, DineroVO b) => new(a.Value + b.Value);
    public static DineroVO operator -(DineroVO a, DineroVO b) => new(a.Value - b.Value);

    public override string ToString() => Value.ToString("C2");
}
