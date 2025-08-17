namespace Usuarios.Domain.Usuarios;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id);
    void Add(Usuario usuario);

}