namespace Domain.ValueObjects;

public record AnioExperienciaVO
{
    public int Value { get; }

    public AnioExperienciaVO(int value)
    {
        if (value >= 100)
        {
            throw new ArgumentException($"No se puede tener mas de 100 anios de experiencia");
        }
        else if (value < 0)
        {
            throw new ArgumentException($"No se puede tener anios de experiencia negativos");
        }
        Value = value;
    }

    public override string ToString() => Value.ToString();
}
