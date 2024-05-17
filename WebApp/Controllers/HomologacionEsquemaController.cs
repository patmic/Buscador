using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
    protected RespuestasAPI? _respuestaApi;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<HomologacionEsquemaController> _logger = logger;

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

      // [Authorize]
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
  }
}