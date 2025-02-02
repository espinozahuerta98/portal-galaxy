﻿using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Interfaces;

public interface IInscripcionProxy : ICrudRestHelper<InscripcionDtoRequest, InscripcionDtoResponse>
{
    Task<PaginationResponse<InscripcionDtoResponse>> ListAsync(BusquedaInscripcionRequest request);

    Task InscripcionMasivaAsync(InscripcionMasivaDtoRequest request);
    Task<Stream> ExportarPdf(BusquedaInscripcionRequest request);
}