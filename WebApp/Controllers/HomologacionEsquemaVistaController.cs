using WebApp.Models;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SharedApp.Models.Dtos;
using SharedApp.Models;
using System.Net;

namespace WebApp.Controllers
{
  [Route("api/homologacion_esquema_vista")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public class HomologacionEsquemaVistaController(ILogger<HomologacionEsquemaVistaController> logger, IHomologacionEsquemaVistaRepository vhRepo, IMapper mapper) : ControllerBase
  {
    private readonly IHomologacionEsquemaVistaRepository _vhRepo = vhRepo;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<HomologacionEsquemaVistaController> _logger = logger;
    [Authorize]
    [HttpGet]
    public IActionResult GethomologacionEsquemaVistas()
    {
        try {
            return Ok(_vhRepo.findAll());
        }
        catch (Exception e) {
            _logger.LogError(e, $"Error en {nameof(GethomologacionEsquemaVistas)}");
            return StatusCode(500, "Error en el servidor");
        }
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GethomologacionEsquemaVista(int Id) {
      try {
        var record = _vhRepo.find(Id);
        if (record == null) {
          return NotFound();
        }

        return Ok(record);
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(GethomologacionEsquemaVista)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpGet("por_vista_esquema/{idVista:int}/{idHomologacionEsquema:int}")]
    public IActionResult findByEsquema(int idVista, int idHomologacionEsquema) {
      try {
        return Ok(_vhRepo.findByEsquema(idVista, idHomologacionEsquema));
      }
      catch (Exception e) {
        _logger.LogError(e, $"Error en {nameof(findByEsquema)}");
        return StatusCode(500, "Error en el servidor");
      }
    }
    [Authorize]
    [HttpPost]
    public IActionResult RegistrarVista([FromBody] List<HomologacionEsquemaVistaDto> _vista)
    {
      var _respuestaApi = new RespuestasAPI();

      try {
          List<HomologacionEsquemaVista> lst = new List<HomologacionEsquemaVista>();

          foreach(var item in _vista) {
            lst.Add(_mapper.Map<HomologacionEsquemaVista>(item));
          }

          if (!_vhRepo.create(lst))
          {
              _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
              _respuestaApi.IsSuccess = false;
              _respuestaApi.ErrorMessages.Add("Error al insertar Homologacion Esquema Vista");
              return BadRequest(_respuestaApi);
          }

          return Ok(_respuestaApi);
      }
      catch (Exception e)
      {
        _logger.LogError(e, $"Error en {nameof(RegistrarVista)}");
        _respuestaApi.ErrorMessages.Add("Error en el servidor");
      }

      return StatusCode(500, _respuestaApi);
    }
  }
}