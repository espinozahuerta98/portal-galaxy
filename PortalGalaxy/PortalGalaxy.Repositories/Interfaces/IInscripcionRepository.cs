﻿using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Entities;

namespace PortalGalaxy.Repositories.Interfaces;

public interface IInscripcionRepository : IRepositoryBase<Inscripcion>
{
    Task<(ICollection<InscripcionInfo> Colecction, int Total)> ListAsync(string? inscrito, string? taller, int? situacion, DateTime? fechaInicio, DateTime? fechaFin, int pagina, int filas);

    Task AddMasivaAsync(ICollection<Inscripcion> inscripciones);
}