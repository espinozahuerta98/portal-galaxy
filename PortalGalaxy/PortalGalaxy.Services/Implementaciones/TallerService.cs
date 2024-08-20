using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Utils;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Services.Implementaciones;

public class TallerService : ITallerService
{
    private readonly ITallerRepository _repository;
    private readonly ILogger<TallerService> _logger;
    private readonly IMapper _mapper;

    public TallerService(ITallerRepository repository, ILogger<TallerService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request)
    {
        var response = new PaginationResponse<TallerDtoResponse>();
        try
        {
            var tupla = await _repository.ListarTalleresAsync(request.Nombre, request.CategoriaId, request.Situacion, request.Pagina, request.Filas);

            response.Data = _mapper.Map<ICollection<TallerDtoResponse>>(tupla.Collection);
            response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar los Talleres";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<PaginationResponse<InscritosPorTallerDtoResponse>> ListAsync(BusquedaInscritosPorTallerRequest request)
    {
        var response = new PaginationResponse<InscritosPorTallerDtoResponse>();

        try
        {
            // Codigo
            var tupla = await _repository.ListAsync(request.InstructorId, request.Taller, request.Situacion,
                request.FechaInicio, request.FechaFin, request.Pagina, request.Filas);

            response.Data = _mapper.Map<ICollection<InscritosPorTallerDtoResponse>>(tupla.Collection);
            response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar los inscritos por taller";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<TallerSimpleDtoResponse>>> ListSimpleAsync()
    {
        var response = new BaseResponseGeneric<ICollection<TallerSimpleDtoResponse>>();

        try
        {
            response.Data = await _repository.ListAsync(
                predicado: x => x.Situacion == SituacionTaller.Aperturada || x.Situacion == SituacionTaller.Por_Aperturar,
                selector: x => new TallerSimpleDtoResponse
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al cargar los talleres";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<PaginationResponse<TallerHomeDtoResponse>> ListarTalleresHomeAsync(BusquedaTallerHomeRequest request)
    {
        var response = new PaginationResponse<TallerHomeDtoResponse>();

        try
        {
            var tupla = await _repository.ListarTalleresHomeAsync(request.Nombre, request.InstructorId,
                request.FechaInicio, request.FechaFin, request.Pagina, request.Filas);

            response.Data = _mapper.Map<ICollection<TallerHomeDtoResponse>>(tupla.Collection);
            response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar los talleres";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponse> AddAsync(TallerDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = _mapper.Map<Taller>(request);

            //entity.PortadaUrl = await _fileUploader.UploadFileAsync(request.Base64Portada, request.ArchivoPortada);
            //entity.TemarioUrl = await _fileUploader.UploadFileAsync(request.Base64Temario, request.ArchivoTemario);

            await _repository.AddAsync(entity);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar un Taller";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<TallerDtoRequest>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<TallerDtoRequest>();
        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "No se encontró el Taller";
                return response;
            }

            response.Data = _mapper.Map<TallerDtoRequest>(entity);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al buscar un Taller";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public Task<BaseResponseGeneric<TallerHomeDtoResponse>> GetTallerHomeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponseGeneric<ICollection<TalleresPorMesDto>>> ReporteTalleresPorMes(int anio)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponseGeneric<ICollection<TalleresPorInstructorDto>>> ReporteTalleresPorInstructor(int anio)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> UpdateAsync(int id, TallerDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "No se encontró el Taller";
                return response;
            }

            _mapper.Map(request, entity);

            if (request.Base64Portada != null)
            {
                //entity.PortadaUrl = await _fileUploader.UploadFileAsync(request.Base64Portada, request.ArchivoPortada);
            }

            if (request.Base64Temario != null)
            {
                //entity.TemarioUrl = await _fileUploader.UploadFileAsync(request.Base64Temario, request.ArchivoTemario);
            }

            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar un Taller";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "No se encontró el Taller";
                return response;
            }

            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar un Taller";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}