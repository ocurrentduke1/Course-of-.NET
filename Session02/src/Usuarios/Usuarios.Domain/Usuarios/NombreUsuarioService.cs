using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class NombreUsuarioService
{
    public Result<NombreUsuario> GenerarNombreUsuario(string nombre, string apellidoPaterno)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno))
        {
            return Result.Failure<NombreUsuario>(UsuarioErrores.ParametrosNombreUsuarioInvalid);
        }

        var nombreUsuario = $"{nombre.Trim()}.{apellidoPaterno.Trim()}".ToUpperInvariant();
        return NombreUsuario.Create(nombreUsuario);
    }
}