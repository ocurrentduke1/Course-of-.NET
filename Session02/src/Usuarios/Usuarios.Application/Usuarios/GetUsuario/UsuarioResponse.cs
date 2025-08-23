//DTO
namespace Usuarios.Application.Usuarios.GetUsuario;

public record UsuarioResponse
(
    string NombrePersona,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string CorreoElectronico,
    DateTime FechaNacimiento,
    string Pais,
    string Departamento,
    string Provincia,
    string Distrito,
    string Calle,
    string Rol,
    DateTime FechaUltimoCambio,
    string Estado
);