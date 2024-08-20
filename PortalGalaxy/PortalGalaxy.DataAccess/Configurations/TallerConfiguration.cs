using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalGalaxy.Entities;

namespace PortalGalaxy.DataAccess.Configurations;

public class TallerConfiguration : IEntityTypeConfiguration<Taller>
{
    public void Configure(EntityTypeBuilder<Taller> builder)
    {
        builder.Property(p => p.PortadaUrl)
            .IsUnicode(false); //VARCHAR EN LUGAR DE NVARCHAR
        
        builder.Property(p => p.TemarioUrl)
            .IsUnicode(false); //VARCHAR EN LUGAR DE NVARCHAR

        builder.Property(p => p.Descripcion)
            .HasMaxLength(700);

        builder.HasIndex(p => p.Nombre);

        builder.HasQueryFilter(p => p.Estado);
    }
}