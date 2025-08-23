using FluentValidation;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(u => u.apellidoPaterno).NotEmpty()
        .WithMessage("The father's surname cannot be left blank");

        RuleFor(u => u.FechaNacimiento).LessThan(DateTime.Now)
        .WithMessage("The date of birth must be in the past");
    }
}