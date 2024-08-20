using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Entities;

namespace PortalGalaxy.Repositories.Interfaces;

public interface IInstructorRepository : IRepositoryBase<Instructor>
{
    Task<ICollection<InstructorInfo>> ListAsync(string? nombre, string? nroDocumento, int? categoriaId);
}