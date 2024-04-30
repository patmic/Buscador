using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/buscador")]
    [ApiController]
    public class BuscadorController : ControllerBase
    {
        private readonly IBuscadorRepository _vhRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscadorController> _logger;

        public BuscadorController(ILogger<BuscadorController> logger, IBuscadorRepository vhRepo, IMapper mapper)
        {
            _vhRepo = vhRepo;
            _mapper = mapper;
            _logger = logger;
        }

        // [Authorize]
        [HttpGet("buscar_palabras")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuscarPalabras(string field, string value)
        {
            try
            {
                Console.WriteLine($"Buscando palabras {field} {value}");
                var listado = _vhRepo.BuscarPalabra(value, field);

                var listadoDto = new List<OrganizacionDto>();

                foreach (var lista in listado)
                {
                    listadoDto.Add(_mapper.Map<OrganizacionDto>(lista));
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