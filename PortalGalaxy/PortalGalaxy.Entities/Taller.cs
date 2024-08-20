namespace PortalGalaxy.Entities;

public class Taller : EntityBase
{
    public string Nombre { get; set; } = default!;
    public Categoria Categoria { get; set; } = default!;
    public int CategoriaId { get; set; }
    public Instructor Instructor { get; set; } = default!;
    public int InstructorId { get; set; }
    public DateOnly FechaInicio { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public SituacionTaller Situacion { get; set; }
    public string? PortadaUrl { get; set; }
    public string? TemarioUrl { get; set; }
    public string? Descripcion { get; set; }

    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
}