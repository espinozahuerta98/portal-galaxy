using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;
using QuestPDF.Fluent;

namespace PortalGalaxy.Services.Interfaces;

public interface IPdfService
{
    Task<BaseResponseGeneric<Document>> Generar(BusquedaTallerRequest request);
    Task<BaseResponseGeneric<Document>> Generar(BusquedaInscripcionRequest request);
    Task<BaseResponseGeneric<Document>> Generar(BusquedaInscritosPorTallerRequest request);
}