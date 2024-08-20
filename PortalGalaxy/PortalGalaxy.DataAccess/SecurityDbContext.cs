using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PortalGalaxy.DataAccess;

public class SecurityDbContext : IdentityDbContext<GalaxyIdentityUser>
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
        :base(options)

    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GalaxyIdentityUser>(e =>
        {
            e.ToTable("Usuario");
            e.Property(p => p.LockoutEnd).HasColumnName("BloqueoHasta");
            e.Property(p => p.AccessFailedCount).HasColumnName("NroIntentos");
        });

        builder.Entity<IdentityRole>(e => e.ToTable("Rol"));
        builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UsuarioRol"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<string>().HaveMaxLength(150);
    }
}