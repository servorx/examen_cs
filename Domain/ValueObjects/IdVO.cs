namespace Domain.ValueObjects;
public record IdVO : IEquatable<IdVO>
{
    public int Value { get; private set; }

    // Constructor usado por EF Core (sin parámetros)
    private IdVO() { }

    // Constructor explícito
    public IdVO(int value)
    {
        Value = value;
    }

    // Método de ayuda
    public static IdVO CreateNew() => new(0);

    public bool IsTemporary => Value <= 0;

    public override string ToString() => Value.ToString();

    // Permitir que EF Core asigne el valor desde la BD
    public void SetValue(int value)
    {
        if (Value == 0)
            Value = value;
    }
}
