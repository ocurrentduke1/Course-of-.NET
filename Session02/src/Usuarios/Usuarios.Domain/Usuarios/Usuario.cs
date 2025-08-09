
using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class Usuario : Entity
{
    public string NombrePersona { get; private set; }
    public string ApellidoPaterno { get; private set; }
    public string ApellidoMaterno { get; private set; }
    public string NombreUsuario { get; private set; }
    public DateTime FechaUltimoCambio { get; private set; }
    public string CorreoElectronico { get; private set; }
    public string Password { get; private set; }
    public int Estado { get; private set; }
    public Direccion Direccion { get; private set; }
}