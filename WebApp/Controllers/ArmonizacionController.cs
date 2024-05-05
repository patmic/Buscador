using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/armonizacion")]
    [ApiController]
    public class ArmonizacionController(ILogger<ArmonizacionController> logger, IArmonizacionRepository vhRepo, IMapper mapper) : ControllerBase
    {
        private readonly IArmonizacionRepository _vhRepo = vhRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ArmonizacionController> _logger = logger;

    // [Authorize]
    [HttpGet("obtener_etiquetas")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerEtiquetas()
        {
            return GetData(_vhRepo.ObtenerEtiquetas, "Error obteniendo datos de pais");
        }

        private IActionResult GetData(Func<IEnumerable<Armonizacion>> getData, string errorMessage)
        {
            try
            {
                var data = getData();

                var dtoList = data.Select(item => _mapper.Map<ArmonizacionDto>(item)).ToList();

                return Ok(dtoList);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new {
                        statusCode = 500,
                        message = errorMessage
                    });
            }
        }
    }
}