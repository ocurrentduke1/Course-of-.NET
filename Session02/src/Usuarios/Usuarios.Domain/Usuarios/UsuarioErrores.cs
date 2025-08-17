using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error PasswordInvalid => new Error("UsuarioErrores.PasswordInvalid", "the password must be at least 6 characters long.");
    public static Error NombreUsuarioInvalid => new Error("UsuarioErrores.NombreUsuarioInvalid", "the username must be at least 3 characters long.");
    public static Error CorreoElectronicoInvalid => new Error("UsuarioErrores.CorreoElectronicoInvalid", "the email address is not valid.");
    public static Error UsuarioIdInvalid => new Error("UsuarioErrores.UsuarioIdInvalid", "the user ID is not valid.");
    public static Error UsuarioNoEncontrado => new Error("UsuarioErrores.UsuarioNoEncontrado", "the user was not found.");
    public static Error CodigoDobleFactorInvalid => new Error("UsuarioErrores.CodigoDobleFactorInvalid", "the two-factor authentication code is not valid.");
    public static Error ParametrosNombreUsuarioInvalid => new Error("UsuarioErrores.ParametrosNombreUsuarioInvalid", "the parameters for generating the username are invalid.");
}