using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
  [Route("api/homologacion_esquema")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public class HomologacionEsquemaController(ILogger<HomologacionEsquemaController> logger, IHomologacionEsquemaRepository vhRepo, IMapper mapper) : ControllerBase
  {
    private readonly IHomologacionEsquemaRepository _vhRepo = vhRepo;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<HomologacionEsquemaController> _logger = logger;

    [Authorize]
    [HttpGet]
    public IActionResult GetHomologacionEsquemas()
    {
      try {
        var records = _vhRepo.findAll();
        var dtos = records.Select(item => _mapper.Map<HomologacionEsquemaDto>(item)).ToList();
        return Ok(dtos);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetHomologacionEsquemas)}");
        return StatusCode(500, "Error en el servidor");
      }
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetHomologacionEsquema(int Id) {
      try {
        var record = _vhRepo.find(Id);
        if (record == null) {
          return NotFound();
        }

        var dto = _mapper.Map<HomologacionEsquemaDto>(record);
        return Ok(dto);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GetHomologacionEsquema)}");
        return StatusCode(500, "Error en el servidor");
      }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult ActualizarHomologacionEsquema(int Id, [FromBody] HomologacionEsquemaDto dto)
    {
      try {
        if (dto == null || Id != dto.IdHomologacionEsquema)
        {
          return BadRequest(ModelState);
        }

        var record = _mapper.Map<HomologacionEsquema>(dto);
        _vhRepo.update(record);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(ActualizarHomologacionEsquema)}");
        return StatusCode(500, "Error en el servidor");
      }
    }

    [Authorize]
    [HttpPost]
    public IActionResult RegistrarHomologacionEsquema([FromBody] HomologacionEsquemaDto dto)
    {
      try {
        var record = _mapper.Map<HomologacionEsquema>(dto);
        _vhRepo.create(record);

        return Ok();
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(RegistrarHomologacionEsquema)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpDelete("{Id:int}")]
    public IActionResult EliminarHomologacionEsquema(int Id)
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
        _logger.LogError(e, $"Error en {nameof(EliminarHomologacionEsquema)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
  }
}