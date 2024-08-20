using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalGalaxy.Entities;

namespace PortalGalaxy.DataAccess.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        var fecha = DateTime.Parse("2024-07-01");
        var usuario = "admin";

        builder.HasData(new List<Categoria>
        {
            new() { Id = 1, Nombre = ".NET", FechaCreacion = fecha, UsuarioCreacion = usuario},
            new() { Id = 2, Nombre = "Java", FechaCreacion = fecha, UsuarioCreacion = usuario },
            new() { Id = 3, Nombre = "AWS", FechaCreacion = fecha, UsuarioCreacion = usuario },
            new() { Id = 4, Nombre = "Azure", FechaCreacion = fecha, UsuarioCreacion = usuario },
            new() { Id = 5, Nombre = "Python", FechaCreacion = fecha, UsuarioCreacion = usuario },
        });

        builder.HasQueryFilter(p => p.Estado);
    }
}