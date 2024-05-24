using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/buscador")]
    [ApiController]
    public class BuscadorController(ILogger<BuscadorController> logger, IBuscadorRepository vhRepo, IMapper mapper) : ControllerBase
    {
        private readonly IBuscadorRepository _vhRepo = vhRepo;
        protected RespuestasAPI? _respuestaApi;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<BuscadorController> _logger = logger;

        // [Authorize]
        [HttpGet("buscar_palabras")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarPalabras(string value, int field1, int field2, int field3, int field4)
        {
            try
            {
                Console.WriteLine($"Buscando palabras {field1} {field2} {field3} {field4} {value}");
                var listado = _vhRepo.BuscarPalabra(value, field1, field2, field3, field4);

                var listadoDto = new List<BuscadorDto>();

                foreach (var lista in listado)
                {
                    listadoDto.Add(_mapper.Map<BuscadorDto>(lista));
                }
                return Ok(listadoDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                            statusCode = 500,
                            message = e.Message
                    });
            }
        }
    
    [HttpGet("organizacion/{Id:int}")]
    public IActionResult BuscarOrganizacion(int Id)
    {
      try
      {
        var organizacion = _vhRepo.BuscarOrganizacion(Id);
        var organizacionDto = _mapper.Map<BuscadorOrganizacionDto>(organizacion);
        return Ok(organizacionDto);
      }
      catch (Exception e)
      {
        _logger.LogError(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, 
          new {
                  statusCode = 500,
                  message = e.Message
          });
      }
    }
  
    [HttpGet("esquemas_relacionados/{Id:int}")]
    public IActionResult ObtenerEsquemasRelacionados(int Id)
    {
        try
        {
          var listado = _vhRepo.ObtenerEsquemasRelacionados(Id);
          var listadoDto = new List<HomologacionEsquemaDto>();

          foreach (var lista in listado)
          {
            listadoDto.Add(_mapper.Map<HomologacionEsquemaDto>(lista));
          }
          return Ok(listadoDto);
        }
        catch (Exception e)
        {
          _logger.LogError(e.Message);
          return StatusCode(StatusCodes.Status500InternalServerError, 
            new {
                    statusCode = 500,
                    message = e.Message
            });
        }
    }

    [HttpGet("organizaciones_relacionadas/{Id}/{IdDataLake}")]
    public IActionResult ObtenerOrganizacionesRelacionadas(int Id, int IdDataLake)
    {
        try
        {
          var listado = _vhRepo.ObtenerOrganizacionesRelacionadas(Id, IdDataLake);
          var listadoDto = new List<DataLakeOrganizacionDto>();

          foreach (var lista in listado)
          {
            listadoDto.Add(_mapper.Map<DataLakeOrganizacionDto>(lista));
          }
          return Ok(listadoDto);
        }
        catch (Exception e)
        {
          _logger.LogError(e.Message);
          return StatusCode(StatusCodes.Status500InternalServerError, 
            new {
                    statusCode = 500,
                    message = e.Message
            });
        }
    }
  }
}