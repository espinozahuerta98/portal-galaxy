using PortalGalaxy.Entities;

namespace PortalGalaxy.Services.Utils;

public static class Helper
{
    public static void InsertarAuditoria(this EntityBase entity, string usuario)
    {
        entity.UsuarioCreacion = usuario;
        entity.FechaCreacion = DateTime.UtcNow;
    }

    public static void ActualizarAuditoria(this EntityBase entity, string usuario)
    {
        entity.UsuarioActualizacion = usuario;
        entity.FechaActualizacion = DateTime.UtcNow;
    }

    public static int GetTotalPages(int totalRows, int rowsPerPage)
    {
        if (totalRows == 0) return 0;
        var totalPages = totalRows / rowsPerPage;
        if (totalPages % rowsPerPage > 0)
        {
            totalPages++;
        }

        return totalPages;
    }
}