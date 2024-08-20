using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Implementaciones;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared;
using PortalGalaxy.Shared.Request;
using QuestPDF.Fluent;
using System.Security.Claims;

namespace PortalGalaxy.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InscripcionesController : ControllerBase
{
    private readonly IInscripcionService _service;
    private readonly IPdfService _pdfService;

    public InscripcionesController(IInscripcionService service, IPdfService pdfService)
    {
        _service = service;
        _pdfService = pdfService;
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync([FromQuery] BusquedaInscripcionRequest request)
    {
        var response = await _service.ListAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] InscripcionDtoRequest request)
    {
        var email = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

        var response = await _service.AddAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("masiva")]
    [Authorize(Roles = Constantes.RolAdministrador)]
    public async Task<IActionResult> PostMasiva([FromBody] InscripcionMasivaDtoRequest request)
    {
        var response = await _service.AddMasivaAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] InscripcionDtoRequest request)
    {
        var response = await _service.UpdateAsync(User.Identity!.Name!, id, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("pdf")]
    public async Task<IActionResult> Pdf(BusquedaInscripcionRequest request)
    {
        var response = await _pdfService.Generar(request);
        if (response.Success)
        {
            var bytes = response.Data.GeneratePdf();

            return File(new MemoryStream(bytes), "application/pdf");
        }

        return Ok(response);
    }
}