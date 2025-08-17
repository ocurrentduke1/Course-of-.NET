using System.Text.RegularExpressions;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class CorreoElectronico
{
    public string Value { get; init; }

    private CorreoElectronico(string value)
    {
        Value = value;
    }

    public static Result<CorreoElectronico> Create(string value)
    {
        if (!IsValid(value))
        {
            return Result.Failure<CorreoElectronico>(UsuarioErrores.CorreoElectronicoInvalid);
        }

        return Result.Success(new CorreoElectronico(value));
    }

    public static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        const string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:[a-zA-Z0-9-]+)*$";
        return Regex.IsMatch(value, emailPattern);
    }
}