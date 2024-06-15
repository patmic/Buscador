using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
  [Route("api/vistas")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public class VistaController(ILogger<VistaController> logger, IVistaRepository vhRepo, IMapper mapper) : ControllerBase
  {
    private readonly IVistaRepository _vhRepo = vhRepo;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<VistaController> _logger = logger;
    [Authorize]
    [HttpGet]
    public IActionResult GetVistas()
    {
        try {
            return Ok(_vhRepo.findAll());
        }
        catch (Exception e) {
            _logger.LogError(e, $"Error en {nameof(GetVistas)}");
            return StatusCode(500, "Error en el servidor");
        }
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetVista(int Id) {
      try {
        var record = _vhRepo.find(Id);
        if (record == null) {
          return NotFound();
        }

        return Ok(record);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpGet("por_sistema/{idHomologacionSistema:int}")]
    public IActionResult GetVistaPorSistema(int idHomologacionSistema) {
      try {
        return Ok(_vhRepo.findBySystem(idHomologacionSistema));
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpGet("propiedades/{vistaNombre}")]
    public IActionResult GetProperties(string vistaNombre) {
      try {
        return Ok(_vhRepo.GetProperties(vistaNombre));
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetProperties)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult ActualizarVista(int Id, [FromBody] Vista _vista)
    {
      try {
        if (_vista == null || Id != _vista.IdVista)
        {
          return BadRequest(ModelState);
        }
        _vhRepo.update(_vista);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(ActualizarVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }

    [Authorize]
    [HttpPost]
    public IActionResult RegistrarVista([FromBody] Vista _vista)
    {
      try {
        _vhRepo.create(_vista);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(RegistrarVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpDelete("{Id:int}")]
    public IActionResult EliminarVista(int Id)
    {
      try {
        var record = _vhRepo.find(Id);

        if (record == null) {
          return NotFound();
        }
        record.Estado = "X";

        _vhRepo.update(record);
        return Ok();
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(EliminarVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
  }
}