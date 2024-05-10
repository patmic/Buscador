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

        [HttpGet("alcance_razon_social")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerAlcanceRazonSocial()
        {
            return GetCatalogData(_vhRepo.ObtenerAlcanceRazonSocial, "Error obteniendo datos de Grilla");
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

        [HttpGet("dimension")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerDimension()
        {
            return GetCatalogData(_vhRepo.ObtenerDimension, "Error obteniendo datos de dimension");
        }

        [HttpGet("etiquetas_grilla")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerEtiquetaGrilla()
        {
            return GetCatalogData(_vhRepo.ObtenerEtiquetaGrilla, "Error obteniendo datos de Etiqueta Grilla");
        }

        [HttpGet("etiquetas_filtro")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ObtenerEtiquetaFiltros()
        {
            return GetCatalogData(_vhRepo.ObtenerEtiquetaFiltros, "Error obteniendo datos de Etiqueta Grilla");
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