using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Shared;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infrastructure.Configurations;

public class UsuarioConfigurations : IEntityTypeConfiguration<Usuario>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.NombrePersona).HasMaxLength(100).IsRequired();
        builder.Property(u => u.ApellidoPaterno).HasMaxLength(100).IsRequired();
        builder.Property(u => u.ApellidoMaterno).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Password)
            .HasMaxLength(20)
            .HasConversion(p => p!.Value, value => Password.Create(value).Value);
        builder.Property(u => u.NombreUsuario)
            .HasMaxLength(20)
            .HasConversion(p => p!.Value, value => NombreUsuario.Create(value).Value);
        builder.Property(u => u.FechaNacimiento).IsRequired();
        builder.Property(u => u.CorreoElectronico)
            .HasMaxLength(50)
            .HasConversion(u => u!.Value, value => CorreoElectronico.Create(value).Value);

        builder.OwnsOne(u => u.Direccion);
        builder.Property(u => u.Estado)
            .HasConversion(
                estado => estado.ToString(),
                estado => ((Estados)Enum.Parse(typeof(Estados), estado))
            ).IsRequired();
        builder.Property(u => u.FechaUltimoCambio).IsRequired();
        builder.HasOne(u => u.Rol)
            .WithMany()
            .HasForeignKey(u => u.RolId)
            .IsRequired();
        builder.Property<uint>("version")
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}