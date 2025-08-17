using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioHandler : ICommandHandler<CrearUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly NombreUsuarioService _nombreUsuarioService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CrearUsuarioHandler(
        IUsuarioRepository usuarioRepository,
        IRolRepository rolRepository,
        IUnitOfWork unitOfWork,
        NombreUsuarioService nombreUsuarioService,
        IDateTimeProvider dateTimeProvider)
    {
        _usuarioRepository = usuarioRepository;
        _rolRepository = rolRepository;
        _unitOfWork = unitOfWork;
        _nombreUsuarioService = nombreUsuarioService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByNameAsync(request.Rol, cancellationToken);

        if (rol is null)
        {
            return Result.Failure<Guid>(RolErrors.RolNoEncontrado);
        }

        var password = Password.Create(request.Password);
        if (password.IsFailure)
        {
            return Result.Failure<Guid>(password.Error);
        }

        var correoElectronico = CorreoElectronico.Create(request.CorreoElectronico);
        if (correoElectronico.IsFailure)
        {
            return Result.Failure<Guid>(correoElectronico.Error);
        }

        var usuario = Usuario.Create(
            request.Nombre,
            request.apellidoPaterno,
            request.apellidoMaterno,
            password.Value,
            _dateTimeProvider.CurrentTime,
            correoElectronico.Value,
            request.FechaNacimiento,
            new Direccion(
                request.Pais,
                request.Departamento,
                request.Ciudad,
                request.Distrito,
                request.Calle),
            rol.Id,
            _nombreUsuarioService);

        if (usuario.IsFailure)
        {
            return Result.Failure<Guid>(usuario.Error);
        }

        _usuarioRepository.Add(usuario.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(usuario.Value.Id);
    }
}