
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Shared;
using Usuarios.Domain.Usuarios.events;

namespace Usuarios.Domain.Usuarios;

public class Usuario : Entity
{
    public readonly List<DobleFactorAutenticacion> autenticacions = new();

    public string? NombrePersona { get; private set; }
    public string? ApellidoPaterno { get; private set; }
    public string? ApellidoMaterno { get; private set; }
    public NombreUsuario? NombreUsuario { get; private set; }
    public Password? Password { get; private set; }
    public DateTime FechaUltimoCambio { get; private set; }
    public CorreoElectronico? CorreoElectronico { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public Estados Estado { get; private set; }
    public Direccion? Direccion { get; private set; }
    public IReadOnlyList<DobleFactorAutenticacion> DobleFactores => autenticacions.AsReadOnly();
    public Guid RolId { get; private set; }
    public Rol? Rol { get; private set; }

    private Usuario() {}
    private Usuario(
        Guid id,
        string? nombrePersona,
        string? apellidoPaterno,
        string? apellidoMaterno,
        NombreUsuario? nombreUsuario,
        Password? password,
        DateTime fechaUltimoCambio,
        CorreoElectronico? correoElectronico,
        DateTime fechaNacimiento,
        Estados estado,
        Direccion? direccion,
        Guid rolId) : base(id)
    {
        NombrePersona = nombrePersona;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        NombreUsuario = nombreUsuario;
        Password = password;
        FechaUltimoCambio = fechaUltimoCambio;
        CorreoElectronico = correoElectronico;
        FechaNacimiento = fechaNacimiento;
        Estado = estado;
        Direccion = direccion;
        RolId = rolId;
    }

    public static Result<Usuario> Create(
        string? nombrePersona,
        string? apellidoPaterno,
        string? apellidoMaterno,
        Password? password,
        DateTime fechaUltimoCambio,
        CorreoElectronico? correoElectronico,
        DateTime fechaNacimiento,
        Direccion? direccion,
        Guid rolId,
        NombreUsuarioService nombreUsuarioService)
    {

        var nombreDeUsuario = nombreUsuarioService.GenerarNombreUsuario(nombrePersona ?? string.Empty, apellidoPaterno ?? string.Empty);

        if (!nombreDeUsuario.IsSuccess)
        {
            return Result.Failure<Usuario>(nombreDeUsuario.Error);
        }

        var usuario = new Usuario(
            Guid.NewGuid(),
            nombrePersona,
            apellidoPaterno,
            apellidoMaterno,
            nombreDeUsuario.Value,
            password,
            fechaUltimoCambio,
            correoElectronico,
            fechaNacimiento,
            Estados.Activo,
            direccion,
            rolId);

        usuario.AddDomainEvent(new UserCreateDomainEvent(usuario.Id));

        return Result.Success(usuario);
    }

    public void InactivarUsuario()
    {
        if (Estado == Estados.Activo)
        {
            Estado = Estados.Inactivo;
            return;
        }
        
        Estado = Estados.Activo;

    }

}