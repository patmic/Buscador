using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
  [Route("api/homologacion")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public class HomologacionController(ILogger<HomologacionController> logger, IHomologacionRepository vhRepo, IMapper mapper) : ControllerBase
  {
    private readonly IHomologacionRepository _vhRepo = vhRepo;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<HomologacionController> _logger = logger;

    [Authorize]
    [HttpGet("findByParent/{valor}")]
    public IActionResult GetHomologacion(int valor)
    {
      try {
        var records = _vhRepo.findByParent(valor);
        var dtos = records.Select(item => _mapper.Map<HomologacionDto>(item)).ToList();
        return Ok(dtos);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetHomologacion)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetHomologacionById(int Id) {
      try {
        var record = _vhRepo.find(Id);
        if (record == null) {
          return NotFound();
        }

        var dto = _mapper.Map<HomologacionDto>(record);
        return Ok(dto);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetHomologacionById)}");
        return StatusCode(500, "Error en el servidor");
      }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult ActualizarHomologacion(int Id, [FromBody] HomologacionDto dto)
    {
      try {
        if (dto == null || Id != dto.IdHomologacion)
        {
          return BadRequest(ModelState);
        }

        var record = _mapper.Map<Homologacion>(dto);
        _vhRepo.update(record);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(ActualizarHomologacion)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpPost]
    public IActionResult RegistrarHomologacion([FromBody] HomologacionDto dto)
    {
      try {
        var record = _mapper.Map<Homologacion>(dto);
        _vhRepo.create(record);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(RegistrarHomologacion)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpDelete("{Id:int}")]
    public IActionResult EliminarHomologacion(int Id)
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
        _logger.LogError(e, $"Error en {nameof(EliminarHomologacion)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
  }
}