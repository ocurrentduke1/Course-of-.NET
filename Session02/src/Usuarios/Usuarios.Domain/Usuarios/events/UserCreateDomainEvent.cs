using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios.events;

public sealed record UserCreateDomainEvent(Guid IdUsuario) : IDomainEvent;