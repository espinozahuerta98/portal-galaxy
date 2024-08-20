using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;
using QuestPDF.Fluent;
using QuestPDF.Previewer;

namespace PortalGalaxy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TalleresController : ControllerBase
{
    private readonly ITallerService _service;
    private readonly IPdfService _pdfService;

    public TalleresController(ITallerService service, IPdfService pdfService)
    {
        _service = service;
        _pdfService = pdfService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
    {
        var response = await _service.ListAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("inscritos")]
    public async Task<IActionResult> Get([FromQuery] BusquedaInscritosPorTallerRequest request)
    {
        var response = await _service.ListAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("simple")]
    public async Task<IActionResult> Get()
    {
        var response = await _service.ListSimpleAsync();

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TallerDtoRequest request)
    {
        var response = await _service.AddAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, TallerDtoRequest request)
    {
        var response = await _service.UpdateAsync(id, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("pdf")]
    public async Task<IActionResult> Pdf(BusquedaTallerRequest request)
    {
        var response = await _pdfService.Generar(request);
        if (response.Success)
        {
            var bytes = response.Data.GeneratePdf();

            return File(new MemoryStream(bytes), "application/pdf");
        }

        return Ok(response);
    }
    [HttpPost("inscritos/pdf")]
    public async Task<IActionResult> Pdf(BusquedaInscritosPorTallerRequest request)
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