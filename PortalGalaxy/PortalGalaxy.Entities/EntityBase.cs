namespace PortalGalaxy.Entities;

public class EntityBase
{
    public int Id { get; set; }
    public bool Estado { get; set; }

    public DateTime FechaCreacion { get; set; }
    public string UsuarioCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }
    public string? UsuarioActualizacion { get; set; }

    protected EntityBase()
    {
        Estado = true;
        FechaCreacion = DateTime.Now;
        UsuarioCreacion = Environment.UserName;
    }
}