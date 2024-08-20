using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;

namespace PortalGalaxy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriasController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _service.ListAsync();

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(CategoriaDtoRequest request)
    {
        var usuario = HttpContext.User.Identity!.Name!;
        var response = await _service.AddAsync(request, usuario);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, CategoriaDtoRequest request)
    {
        var usuario = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Name).Value;

        var response = await _service.UpdateAsync(id, request, usuario);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);

        return Ok(response);
    }
}