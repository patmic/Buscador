using WebApp.Models;
using WebApp.Models.Dtos;
using WebApp.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/catalogos")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly IVwHomologacionRepository _vhRepo;
        protected RespuestasAPI _respuestaApi;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogosController> _logger;

        public CatalogosController(ILogger<CatalogosController> logger, IVwHomologacionRepository vhRepo, IMapper mapper)
        {
            _vhRepo = vhRepo;
            _mapper = mapper;
            _logger = logger;
        }

        // [Authorize]
        [HttpGet("pais")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerPais()
        {
            return GetCatalogData(_vhRepo.ObtenerPais, "Error obteniendo datos de pais");
        }

        [HttpGet("razon_social")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerRazonSocial()
        {
            return GetCatalogData(_vhRepo.ObtenerRazonSocial, "Error obteniendo datos de razon social");
        }

        [HttpGet("alcance")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerAlcance()
        {
            return GetCatalogData(_vhRepo.ObtenerAlcance, "Error obteniendo datos de alcance");
        }

        [HttpGet("esq_acredita")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerEsqAcredita()
        {
            return GetCatalogData(_vhRepo.ObtenerEsqAcredita, "Error obteniendo datos de esquema acredita");
        }

        [HttpGet("estado")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerEstado()
        {
            return GetCatalogData(_vhRepo.ObtenerEstado, "Error obteniendo datos de estado");
        }

        [HttpGet("org_acredita")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult  ObtenerOrgAcredita()
        {
            return GetCatalogData(_vhRepo.ObtenerOrgAcredita, "Error obteniendo datos de organizacion acredita");
        }

        private IActionResult GetCatalogData(Func<IEnumerable<IVwHomologacion>> getData, string errorMessage)
        {
            try
            {
                var data = getData();

                var catalogDtoList = data.Select(item => _mapper.Map<CatalogosDto>(item)).ToList();

                return Ok(catalogDtoList);
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