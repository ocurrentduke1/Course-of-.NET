using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Roles;

public static class RolErrors
{
        public static Error RolNombreInvalid => new Error("RolErrores.RolNameInvalid", "the RolName is invalid");
        public static Error RolDescripcionInvalid => new Error("RolErrores.RolDescriptionInvalid", "the RolDescription is invalid");
        public static Error RolNoEncontrado => new Error("RolErrores.RolNotFound", "the Rol was not found");
}