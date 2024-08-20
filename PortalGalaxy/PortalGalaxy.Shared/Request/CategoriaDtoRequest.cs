using System.ComponentModel.DataAnnotations;

namespace PortalGalaxy.Shared.Request;

public class CategoriaDtoRequest
{
    [Required(ErrorMessage = Constantes.CampoRequerido)]
    [StringLength(100, ErrorMessage = Constantes.CampoLargo)]
    public string Nombre { get; set; } = default!;
}