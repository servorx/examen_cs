namespace Domain.ValueObjects;

public record CorreoVO
{
    public string Value { get; }

    public CorreoVO(string value)
    {
        // el correo no puede estar vacío ni nulo.
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El correo no puede estar vacío.");

        // Expresión regular mejorada para validar correos electrónicos reales.
        // Permite subdominios, puntos y caracteres especiales válidos en la parte local.
        // Ejemplos válidos:
        // - usuario@gmail.com
        // - usuario.principal@empresa.co
        // - user+123@sub.dominio.org
        var emailPattern = @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$";

        // Si no cumple el formato, lanza excepción.
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, emailPattern))
            throw new ArgumentException("El formato del correo electrónico es inválido.");
        // si el corres es demasiadlo largo, lanza excepción.
        if (value.Length > 254)
            throw new ArgumentException("El correo electrónico no puede exceder los 254 caracteres.");
        // Normaliza el valor: elimina espacios y convierte a minúsculas.
        Value = value.Trim().ToLowerInvariant();
    }

    public override string ToString() => Value;
}