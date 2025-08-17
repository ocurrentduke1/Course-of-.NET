using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public sealed record CrearUsuarioCommand
(
    string Nombre,
    string apellidoPaterno,
    string apellidoMaterno,
    string Password,
    string CorreoElectronico,
    DateTime FechaNacimiento,
    string Pais,
    string Departamento,
    string Ciudad,
    string Distrito,
    string Calle,
    string Rol
) : ICommand<Guid>;